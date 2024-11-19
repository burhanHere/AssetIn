import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { formControlValueMatch } from '../../../shared/validators/form-control-value-match.validator';
import { ResetPassword } from '../../../core/models/reset-password';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { ApiResponse } from '../../../core/models/apiResponse';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
})
export class ResetPasswordComponent implements OnInit {
  private activeRoute: ActivatedRoute = inject(ActivatedRoute);
  private authenticationService: AuthenticationService = inject(
    AuthenticationService
  );
  private router: Router = inject(Router);
  private token: string = '';
  private email: string = '';
  public passwordResetFrom: FormGroup = new FormGroup({
    newPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(11),
      Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{11,}$/),
    ]),
    confirmNewPassowrd: new FormControl('', [Validators.required]),
  });
  public isLoading: boolean = false;
  public showAlertCard: boolean = false;
  public alertCardTitle: string = '';
  public alertCardMessage: string = '';

  ngOnInit(): void {
    debugger;
    this.passwordResetFrom
      .get('confirmNewPassowrd')
      ?.setValidators([
        Validators.required,
        formControlValueMatch(this.passwordResetFrom.get('newPassowrd')!),
      ]); // Apply the custom validator here
    // this.activeRoute.queryParams.subscribe((params) => {
    //   this.token = encodeURIComponent(params['token']);
    //   this.email = encodeURIComponent(params['email']);
    // });
    this.email = this.activeRoute.snapshot.queryParams['email'];
    this.token = this.activeRoute.snapshot.queryParams['token'];
  }

  public resetPassowrd(): void {
    debugger;
    if (this.passwordResetFrom.valid) {
      this.isLoading = true;
      const resetPassowrdData: ResetPassword = {
        email: this.email,
        token: this.token,
        newPassword: this.passwordResetFrom.controls['newPassword']?.value,
      };
      this.authenticationService.ResetPassword(resetPassowrdData).subscribe(
        (response: ApiResponse) => {
          this.alertCardTitle = 'Success 🎉🎉';
          this.alertCardMessage = response.responseData[0];
          this.isLoading = false;
          this.showAlertCard = true;
        },
        (error: HttpErrorResponse) => {
          if (error.status === 400 || error.status === 404) {
            this.alertCardTitle = error.status.toString();
            this.alertCardMessage = error.error.responseData[0];
            this.isLoading = false;
            this.showAlertCard = true;
          }
          this.router.navigateByUrl('**');
        }
      );
    }
  }

  public dismissAlertAndLogin(event: boolean): void {
    this.showAlertCard = event;
    this.router.navigateByUrl('auth/signIn');
  }
}
