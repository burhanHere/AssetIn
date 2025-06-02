import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse';
import { NewAssetRequest } from '../../models/newAssetRequest';

@Injectable({
  providedIn: 'root'
})
export class AssetRequestManagementService {

  private httpCient: HttpClient = inject(HttpClient);
  private apiUrls: any = ApiUrls;

  public CreateAssetRequest(assetRequest: NewAssetRequest): Observable<ApiResponse> {
    return this.httpCient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.CreateAssetRequest, assetRequest);
  }

  public GetAllAssetRequestAdminList(organizationId: number): Observable<ApiResponse> {
    return this.httpCient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.GetAllAssetRequestAdminList + `?organizationId=${organizationId}`);
  }

  public GetAllAssetRequestEmployeeListStatsAndDesignatedAssets(organizationId: number): Observable<ApiResponse> {
    return this.httpCient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.GetAllAssetRequestEmployeeListStatsAndDesignatedAssets + `?organizationId=${organizationId}`);
  }

  public UpdateAssetRequestStatusToAccepted(assetRequestId: number): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.UpdateAssetRequestStatusToAccepted + `?assetRequestId=${assetRequestId}`, null);
  }

  public UpdateAssetRequestStatusToDeclined(assetRequestId: number): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.UpdateAssetRequestStatusToDeclined + `?assetRequestId=${assetRequestId}`, null);
  }

  public UpdateAssetRequestStatusToFulfilled(assetRequestId: number): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.UpdateAssetRequestStatusToFulfilled + `?assetRequestId=${assetRequestId}`, null);
  }

  public UpdateAssetRequestStatusToCanceled(assetRequestId: number): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetRequestManagement.UpdateAssetRequestStatusToCanceled + `?assetRequestId=${assetRequestId}`, null);
  }
}
