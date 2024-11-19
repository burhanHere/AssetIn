import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthRoutingModule } from './auth-routing.module';

import { SignInComponent } from './sign-in/sign-in.component';
import { AlertCardComponent } from '../../shared/components/alert-card/alert-card.component';
import { PageLoaderComponent } from '../../shared/components/page-loader/page-loader.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
  declarations: [SignInComponent, SignUpComponent, ForgotPasswordComponent, EmailConfirmationComponent, ResetPasswordComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    RouterLink,
    ReactiveFormsModule,
    AlertCardComponent,
    PageLoaderComponent,
  ],
})
export class AuthModule {}
