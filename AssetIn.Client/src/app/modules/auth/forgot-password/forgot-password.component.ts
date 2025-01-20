import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { ApiResponse } from '../../../core/models/apiResponse';
import { HttpErrorResponse } from '@angular/common/http';
import { registerAppScopedDispatcher } from '@angular/core/primitives/event-dispatch';
import { Router } from '@angular/router';
import { ForgetPassword } from '../../../core/models/forget-password';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css',
})
export class ForgotPasswordComponent {
  private authenticationService: AuthenticationService = inject(
    AuthenticationService
  );
  private router: Router = inject(Router);
  public forgetPasswordForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  });
  public showAlertCard: boolean = false;
  public alertCardTitle: string = '';
  public alertCardMessage: string = '';
  public isLoading: boolean = false;

  public GetPasswordResetLink(): void {
    if (this.forgetPasswordForm.valid) {
      this.isLoading = true;
      const forgetPassword: ForgetPassword = {
        email: this.forgetPasswordForm.controls['email'].value
      };
      this.authenticationService.ForgetPassword(forgetPassword).subscribe(
        (response: ApiResponse) => {
          this.alertCardTitle = 'Successful 🎉';
          this.alertCardMessage = response.responseData[0];
          this.showAlertCard = true;
          this.isLoading = false;
        },
        (error: HttpErrorResponse) => {
          if (error.status === 400 || error.status === 404) {
            this.alertCardTitle = error.status.toString();
            this.alertCardMessage = error.error.responseData[0];
            this.showAlertCard = true;
            this.isLoading = false;
          } else {
            // some other error occures
            this.router.navigateByUrl('**');
          }
        }
      );
      this.forgetPasswordForm.reset();
    }
  }

  public dismissAlertAndLogin(event: boolean): void {
    this.showAlertCard = event;
    this.router.navigateByUrl('auth/signIn');
  }
}
