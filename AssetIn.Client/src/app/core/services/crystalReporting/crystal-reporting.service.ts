import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiUrls } from '../../constants/api-urls';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class CrystalReportingService {
  private httpCient: HttpClient = inject(HttpClient);
  private apiUrls: any = ApiUrls;


  public GetFilterData(organizationId: number): Observable<ApiResponse> {
    return this.httpCient.get<ApiResponse>(this.apiUrls.baseUrl + this.apiUrls.CrystalReporting.GetFilterData + `?organizationId=${organizationId}`);
  }

  public GenerateHtmlReportByFilter(reportFilterationData: any) {
    return this.httpCient.post(this.apiUrls.baseUrl + this.apiUrls.CrystalReporting.GenerateHtmlReportByFilter, reportFilterationData);
  }

}
