import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  private router: Router = inject(Router);
  public signUpForm: FormGroup = new FormGroup({
    fullName: new FormControl('', [Validators.required]),
    userName: new FormControl('', [Validators.required]),
    gender: new FormControl('', [Validators.required]),
    dateOfBirth: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl('', [
      Validators.required,
      Validators.maxLength(13),
      Validators.minLength(13),
      Validators.pattern(
        /^\+(\d{1,3})\s?\(?\d{1,4}\)?[\s\-]?\d{1,4}[\s\-]?\d{1,4}[\s\-]?\d{1,4}$/
      ),
    ]),
    requireRole: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(11),
      Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/),
    ]),
    confirmPassword: new FormControl('', [Validators.required]),
  });
  public isLoading: boolean = false;
  public showAlert: boolean = false;
  public alertTitle: string = '';
  public alertMessage: string = '';

  public signUpUser(): void {
    console.log(this.signUpForm.value);
  }
}
