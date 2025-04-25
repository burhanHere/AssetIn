import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { SignUp } from '../../../core/models/sign-up';
import { ApiResponse } from '../../../core/models/apiResponse';
import { HttpErrorResponse } from '@angular/common/http';
import { formControlValueMatch } from '../../../shared/validators/form-control-value-match.validator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent implements OnInit {
  private router: Router = inject(Router);
  private authenticationService: AuthenticationService = inject(AuthenticationService);
  public signUpForm: FormGroup;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertTitle: string;
  public alertMessage: string;
  constructor() {
    this.signUpForm = new FormGroup({
      fullName: new FormControl('', [Validators.required]),
      userName: new FormControl('', [Validators.required]),
      gender: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [
        Validators.required,
        Validators.maxLength(13),
        Validators.minLength(13),
        Validators.pattern(/^\+\d{1,3}\d{9,12}$/),
      ]),
      requiredRole: new FormControl('', [Validators.required]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(11),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{11,}$/),
      ]),
      confirmPassword: new FormControl('', [Validators.required]),
    });
    this.isLoading = false;
    this.showAlert = false;
    this.alertTitle = '';
    this.alertMessage = '';
  }

  ngOnInit(): void {
    this.signUpForm
      .get('confirmPassword')
      ?.setValidators([
        Validators.required,
        formControlValueMatch(this.signUpForm.get('password')!),
      ]);
  }

  public signUpUser(): void {
    if (this.signUpForm.valid) {
      this.isLoading = true;
      let userData: SignUp = this.signUpForm.value;
      this.authenticationService.SignUp(userData).subscribe(
        (response: ApiResponse) => {
          this.isLoading = false;
          this.showAlert = true;
          this.alertTitle = 'Successful🎉.';
          this.alertMessage = response.responseData[0];
        },
        (error: HttpErrorResponse) => {
          this.isLoading = false;
          this.showAlert = true;
          if (error.status === 409 || error.status === 400) {
            // 409conflict User email already registered.
            // 400BadRequest Unable to create new user
            this.alertTitle = error.status.toString();
            this.alertMessage = error.error.responseData[0];
          } else {
            // redirecting to error page
            this.router.navigateByUrl('**');
          }
        }
      );
      this.signUpForm.reset();
    }
  }

  public dismissAlertAndLogin(event: boolean): void {
    this.showAlert = event;
    this.router.navigateByUrl('auth/signIn');
  }
}
