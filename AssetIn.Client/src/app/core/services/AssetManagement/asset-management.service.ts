import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { ApiResponse } from '../../models/apiResponse';
import { Observable } from 'rxjs';
import { checkOut } from '../../models/checkOut';
import { checkIn } from '../../models/checkIn';
import { retireAsset } from '../../models/retireAsset';
import { sendToMaintenance } from '../../models/sendToMaintenance';
import { returnFromMaintenance } from '../../models/returnFromMaintenance';
import { updateAsset } from '../../models/updateAsset';

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

  public RetireAsset(assetID: retireAsset): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.RetireAsset, assetID);
  }

  public CheckOutAsset(checkOut: checkOut): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.CheckOutAsset, checkOut);
  }

  public CheckInAsset(checkIn: checkIn): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.CheckInAsset, checkIn);
  }

  public SendAssetToMaintenance(sendToMaintenance: sendToMaintenance): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.SendAssetToMaintenance, sendToMaintenance);
  }

  public ReturnAssetFromMaintenance(returnFromMaintenance: returnFromMaintenance): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.ReturnAssetFromMaintenance, returnFromMaintenance);
  }

  public GetAllAssetStatus(organizationID: number): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.GetAllAssetStatus + `?organizationID=${organizationID}`);
  }

  public updateAsset(asset: updateAsset): Observable<ApiResponse> {
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.AssetManagement.UpdateAsset, asset);
  }

}

