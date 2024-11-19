import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
  { path: '', redirectTo: 'signIn', pathMatch: 'full' },
  {
    path: 'signIn',
    component: SignInComponent,
    title: 'SignIn'
  },
  {
    path: 'signUp',
    component: SignUpComponent,
    title:'SignUp'
  },
  {
    path: 'forgotPassword',
    component: ForgotPasswordComponent,
    title:'ForgotPassword'
  },
  {
    path: 'resetPassword',
    component: ResetPasswordComponent,
    title:'ResetPassword'
  },
  {
    path: 'emailConfirmation',
    component: EmailConfirmationComponent,
    title:'Email Confirmation'
  },
  {
    path: '**',
    component: ErrorPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
