import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse';
import { Organization } from '../../models/organization';

@Injectable({
  providedIn: 'root'
})
export class OrganizationManagementService {
  // Inject the HttpClient service for making HTTP requests to the backend API
  private httpClient: HttpClient = inject(HttpClient);
  // Define API URLs (replace `ApiUrls` with your actual implementation)
  private apiUrls: any = ApiUrls;

  public CreateOrganization(newOrganization: Organization): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.OrganizationManagement.CreateOrganization, newOrganization);
  }

  public UpdateOrganization(updateOrganization: Organization): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.OrganizationManagement.UpdateOrganization, updateOrganization);
  }

  public DeleteOrganization(targetOrganizationId: number): Observable<ApiResponse> {
    return this.httpClient.delete<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.OrganizationManagement.DeleteOrganization + `?organizationId=${targetOrganizationId}`);
  }

  public GetOrganizationInfoForOrganizationDashboard(targetOrganizationId: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.OrganizationManagement.GetOrganizationInfoForOrganizationDashboard + `?OrganizationID=${targetOrganizationId}`);

  }

  public GetOrganizationsListForOrganizationsDashboard(): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.OrganizationManagement.GetOrganizationsListForOrganizationsDashboard);
  }
}
