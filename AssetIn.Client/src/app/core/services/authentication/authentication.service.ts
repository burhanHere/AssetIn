import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignIn } from '../../models/signIn';

import { ApiUrls } from '../../constants/api-urls';
import { ApiResponse } from '../../models/apiResponse';

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
  // public SignUp(): Observable<ApiResponse> {
  //   return Object();
  // }

  // Method for user email confirmation using HTTP GET request with user data
  // public ConfirmEmail(): Observable<ApiResponse> {
  //   return Object();
  // }

  // Method for user sign in using HTTP POST request with user data
  public SignIn(userData: SignIn): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(
      this.apiUrls.baseUrl + this.apiUrls.authentionApiUrls.SignIn,
      userData
    );
  }

  // Method for forget password request using HTTP POST request with user data
  // public ForgetPassword(): Observable<ApiResponse> {
  //   return Object();
  // }

  // Method for user reset password using HTTP POST request with user data
  // public ResetPassword(): Observable<ApiResponse> {
  //   return Object();
  // }
}
