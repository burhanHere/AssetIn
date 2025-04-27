import { Component, inject, OnInit } from '@angular/core';
import { OrganizationManagementService } from '../../../../core/services/organizationManagement/organization-management.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-organization-admin-dashboard',
  templateUrl: './organization-admin-dashboard.component.html',
  styleUrl: './organization-admin-dashboard.component.css'
})
export class OrganizationAdminDashboardComponent implements OnInit {
  private router: Router = inject(Router)
  private organizationManagementService: OrganizationManagementService = inject(OrganizationManagementService);
  private organizationId: number;

  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public organizationData: any;

  constructor() {
    const temp = sessionStorage.getItem('targetOrganizationID');
    this.organizationId = Number(temp === null || temp === undefined ? 0 : temp);
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.organizationData = null;
  }

  ngOnInit(): void {
    if (this.organizationId === 0) {
      this.router.navigateByUrl('**');
    } else {
      this.organizationManagementService.GetOrganizationInfoForOrganizationDashboard(this.organizationId).subscribe(
        (responce: any) => {
          this.isLoading = false;
          this.organizationData = responce.responseData;
        },
        (error: HttpErrorResponse) => {
          this.isLoading = false;
          this.showAlert = true;
          this.alertMessage = error.error.responseData[0] || 'Error';
          this.alertTitle = error.error.responseData[1] || 'An error occurred.';
        }
      );
    }
  }



}
