import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class VendorManagementService {
  // Inject the HttpClient service for making HTTP requests to the backend API
  private httpClient: HttpClient = inject(HttpClient);
  // Define API URLs (replace `ApiUrls` with your actual implementation)
  private apiUrls: any = ApiUrls;

  public GetVendorInfo(): Observable<ApiResponse> {
    // Make a GET request to the backend API to retrieve vendor information
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.VendorManagement.GetVendorInfo);
  }
  public CreateUpdateVendorInfo(vendorData: any): Observable<ApiResponse> {
    // Make a PUT request to the backend API to create or update vendor information
    return this.httpClient.put<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.VendorManagement.CreateUpdateVendorInfo, vendorData);
  }

  public CreateVendorProduct(productData: any): Observable<ApiResponse> {
    // Make a POST request to the backend API to create a new vendor product
    return this.httpClient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.VendorManagement.CreateVendorProduct, productData);
  }

  public GetVendorProducts(): Observable<ApiResponse> {
    // Make a GET request to the backend API to retrieve vendor products
    return this.httpClient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.VendorManagement.GetVendorProducts);
  }

  public UploadVendorProfilePicture(file: any): Observable<ApiResponse> {
    // Make a POST request to the backend API to upload the vendor profile picture
    return this.httpClient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.VendorManagement.UploadVendorProfilePicture, file);
  }
}
