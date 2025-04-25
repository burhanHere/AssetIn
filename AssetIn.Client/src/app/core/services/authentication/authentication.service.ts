import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignIn } from '../../models/sign-in';

import { ApiUrls } from '../../constants/api-urls';
import { ApiResponse } from '../../models/apiResponse';
import { SignUp } from '../../models/sign-up';
import { ForgetPassword } from '../../models/forget-password';
import { ResetPassword } from '../../models/reset-password';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  // Inject the HttpClient service for making HTTP requests to the backend API
  private httpClient: HttpClient = inject(HttpClient);
  // Define API URLs (replace `ApiUrls` with your actual implementation)
  private apiUrls: any = ApiUrls;

  // Public methods for authentication functionalities

  // Method for new user sign up using HTTP POST request with user data
  public SignUp(userData: SignUp): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(
      this.apiUrls.baseUrl + this.apiUrls.Authentication.SignUp,
      userData
    );
  }

  // Method for user email confirmation using HTTP GET request with user data
  public ConfirmEmail(token: string, email: string): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(
      this.apiUrls.baseUrl +
        this.apiUrls.Authentication.ConfirmEmail +
        `?token=${token}&email=${email}`
    );
  }

  // Method for user sign in using HTTP POST request with user data
  public SignIn(userData: SignIn): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(
      this.apiUrls.baseUrl + this.apiUrls.Authentication.SignIn,
      userData
    );
  }

  // Method for forget password request using HTTP POST request with user data
  public ForgetPassword(userData: ForgetPassword): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(
      this.apiUrls.baseUrl + this.apiUrls.Authentication.ForgetPassword,
      userData
    );
  }

  // Method for user reset password using HTTP POST request with user data
  public ResetPassword(userData: ResetPassword): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(this.apiUrls.baseUrl+this.apiUrls.Authentication.ResetPassword,userData);
  }
}
