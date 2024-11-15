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
  private authenticationServiceService: AuthenticationService = inject(
    AuthenticationService
  );
  private router: Router = inject(Router);
  public loginForm: FormGroup = new FormGroup({
    Email: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required]),
  });
  public isLoading: boolean = false;
  public showAlertCard: boolean = false;
  public errorCardMessage: string = '';
  public errorCardTitle: string = '';

  signIn(): void {
    if (!this.loginForm.invalid) {
      this.isLoading = true;
      // console.log(this.loginForm.value);
      this.authenticationServiceService.SignIn(this.loginForm.value).subscribe(
        (response: ApiResponse) => {
          this.isLoading = false;
          // console.log(response);
          this.errorCardTitle = 'Congratulations 🎉';
          this.errorCardMessage = response.responseData['message'];
          this.showAlertCard = true;
          localStorage.setItem('auth', response.responseData['jwt']);
        },
        (error: HttpErrorResponse) => {
          this.isLoading = false;
          // console.log(error);
          if (error['status'] == 500) {
            // redirecting to error page
            this.router.navigateByUrl('**');
          } else {
            // all good
            // ----------------------
            //401unauthorized-> wrong password
            //403forbidden-> unable to confirm email or email is not confirmed yet
            //404not found-> user not found
            this.errorCardTitle = error.error.status.toString();
            this.errorCardMessage = error.error.responseData[0];
            this.showAlertCard = true;
          }
        }
      );
      this.loginForm.reset();
    }
  }
}
