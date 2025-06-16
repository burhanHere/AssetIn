import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { ApiResponse } from '../../models/apiResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssetManagementService {
  // Inject the HttpClient service for making HTTP requests to the backend API
  private httpClient: HttpClient = inject(HttpClient);
  // Define API URLs (replace `ApiUrls` with your actual implementation)
  private apiUrls: any = ApiUrls;

  public GetAllAsset(organizationID: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAllAsset + `?organizationId=${organizationID}`);
  }

  public GetAsset(targetAssetId: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAsset + `?assetID=${targetAssetId}`);
  }

  public DeleteAsset(targetAssetId: number): Observable<ApiResponse> {
    return this.httpClient.delete<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.DeleteAsset + `?assetID=${targetAssetId}`);
  }

  public GetAllAssetCatagory(organizationID: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAllAssetCatagory + `?organizationID=${organizationID}`);
  }
}
