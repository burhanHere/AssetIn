import { Component, inject } from '@angular/core';
import { OnInit } from '@angular/core';
import { OrganizationManagementService } from '../../../../core/services/organizationManagement/organization-management.service';
import { ApiResponse } from '../../../../core/models/apiResponse';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrl: './vendors.component.css',
})
export class VendorsComponent implements OnInit {
  private organizationManagementService: OrganizationManagementService = inject(OrganizationManagementService);
  public vendorsAndProducts: any[];
  public selectedVendor: any;
  public showVendor: boolean;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;

  constructor() {
    this.vendorsAndProducts = [];
    this.selectedVendor = {};
    this.showVendor = false;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.organizationManagementService.GetVendorAndVendorProducts().subscribe(
      (response: ApiResponse) => {
        debugger;
        this.vendorsAndProducts = response.responseData?.vendorsWithProducts;
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        debugger;
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.showAlert = true;
        this.isLoading = false;
      }
    );
  }

  public showVendorDetails(vendor: any): void {
    // get vendor info
    // get product info
    this.selectedVendor = vendor;
    this.showVendor = true;

  }
}
