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

  public GetAllAvailableAssetByCatagoryId(organizationID: number, catagoryID: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAllAvailableAssetByCatagoryId + `?organizationID=${organizationID}&catagoryID=${catagoryID}`);
  }

  public GetAllAssetCatagory(organizationID: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAllAssetCatagory + `?organizationID=${organizationID}`);
  }

  public GetAllAssetType(organizationID: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAllAssetType + `?organizationID=${organizationID}`);
  }

  public CreateNewAssetType(assetType: any): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.CreateNewAssetType, assetType);
  }

  public CreateNewAssetCatagory(assetCatagory: any): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.CreateNewAssetCatagory, assetCatagory);
  }

  public CreateAsset(asset: any): Observable<ApiResponse> {
    return this.httpClient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.CreateAsset, asset);
  }
}

