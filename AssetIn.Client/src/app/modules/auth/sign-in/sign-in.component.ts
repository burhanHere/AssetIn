import { Component } from '@angular/core';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { inject } from '@angular/core';

import { Router } from '@angular/router';
import { ApiResponse } from '../../../core/models/apiResponse';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
})
export class SignInComponent {
  private authenticationService: AuthenticationService = inject(
    AuthenticationService
  );
  private router: Router = inject(Router);
  public loginForm: FormGroup = new FormGroup({
    Email: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required]),
  });
  public isLoading: boolean = false;
  public showAlertCard: boolean = false;
  public alertCardMessage: string = '';
  public alertCardTitle: string = '';

  public signIn(): void {
    if (!this.loginForm.invalid) {
      this.isLoading = true;
      this.authenticationService.SignIn(this.loginForm.value).subscribe(
        (response: ApiResponse) => {
          this.isLoading = false;
          this.alertCardTitle = 'Congratulations 🎉';
          this.alertCardMessage = response.responseData['message'];
          this.showAlertCard = true;
          localStorage.setItem('auth', response.responseData['jwt']);
        },
        (error: HttpErrorResponse) => {
          this.isLoading = false;
          if (
            error.status === 401 ||
            error.status === 403 ||
            error.status === 404
          ) {
            // all good
            // ----------------------
            //401unauthorized-> wrong password
            //403forbidden-> unable to confirm email or email is not confirmed yet
            //404not found-> user not found
            this.alertCardTitle = error.error.status.toString();
            this.alertCardMessage = error.error.responseData[0];
            this.showAlertCard = true;
          } else {
            // redirecting to error page
            this.router.navigateByUrl('**');
          }
        }
      );
      this.loginForm.reset();
    }
  }
}
