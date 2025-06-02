import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse';
import { NewEmployee } from '../../models/newEmployee';
import { lockUnlockUser } from '../../models/lockUnlockUser';

@Injectable({
  providedIn: 'root'
})
export class EmployeeManagementService {

  private httpCient: HttpClient = inject(HttpClient);
  private apiUrls: any = ApiUrls;

  public CreateEmployee(newEmployee: NewEmployee): Observable<ApiResponse> {
    return this.httpCient.post<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.EmployeeManagement.CreateEmployee, newEmployee);
  }

  public GetEmployeeList(organizationId: number): Observable<ApiResponse> {
    return this.httpCient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.EmployeeManagement.GetEmployeeList + `?organizationId=${organizationId}`);
  }

  public LockUserAcount(lockuser:lockUnlockUser): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.EmployeeManagement.LockUserAccount, lockuser);
  }

  public UnlockUserAcount(Unlockuser:lockUnlockUser): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.EmployeeManagement.UnlockUserAccount, Unlockuser);
  }

  public RevokeAssetManagerPreviliges(RevokeUser:lockUnlockUser): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.EmployeeManagement.RevokeAssetManagerPreviliges, RevokeUser);
  }

  public GrantAssetManagerPreviliges(GrantUser:lockUnlockUser): Observable<ApiResponse> {
    return this.httpCient.patch<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.EmployeeManagement.GrantAssetManagerPreviliges, GrantUser);
  }
}
