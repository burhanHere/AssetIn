import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { ApiResponse } from '../../models/apiResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserManagementService {
  // Inject the HttpClient service for making HTTP requests to the backend API
  private httpClient: HttpClient = inject(HttpClient);
  // Define API URLs (replace `ApiUrls` with your actual implementation)
  private apiUrls: any = ApiUrls;

  public GetUserInfo(): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.UserManagement.GetUserInfo);
  }

  public UpdateUserProfilePicture(file: any): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.UserManagement.UpdateUserProfilePicture, file);
  }
}
