import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AssetRequestManagementService } from '../../../../core/services/AssetRequestManagement/asset-request-management.service';
import { AssetManagementService } from '../../../../core/services/AssetManagement/asset-management.service';
@Component({
  selector: 'app-asset-requests',
  templateUrl: './asset-requests.component.html',
  styleUrl: './asset-requests.component.css',
})
export class AssetRequestsComponent implements OnInit {
  private assetRequestManagementService: AssetRequestManagementService = inject(
    AssetRequestManagementService
  );
  private assetManagementService: AssetManagementService = inject(
    AssetManagementService
  );

  private organizationId: number;
  public activeFilter: string;
  public dashboardData: any;
  public requests: any[];
  public filteredRequests: any[];
  public assignAssetForm: FormGroup;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public showAssignAssetModal: boolean;
  public showViewModal: boolean;
  public selectedRequest: any;
  public availableAssetsCategory: any[];
  public availableAssets: any[];

  constructor() {
    this.assignAssetForm = new FormGroup({
      category: new FormControl('', [Validators.required]),
      availableAssets: new FormControl('', [Validators.required]),
      notes: new FormControl(''),
    });
    this.activeFilter = 'All';
    this.dashboardData = {};
    this.requests = [];
    this.selectedRequest = {};
    this.filteredRequests = [];
    this.showAssignAssetModal = false;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.showViewModal = false;
    this.organizationId =
      Number(sessionStorage.getItem('targetOrganizationID')) || 0;
    this.availableAssetsCategory = [];
    this.availableAssets = [];
  }

  ngOnInit(): void {
    this.getallAssetrequest();
  }

  public getallAssetrequest(): void {
    this.isLoading = true;
    this.showAlert = false;
    this.assetRequestManagementService
      .GetAllAssetRequestAdminList(this.organizationId)
      .subscribe(
        (response: any) => {
          this.requests = this.filteredRequests = response.responseData.sort(
            (a: any, b: any) =>
              new Date(b.requestDate).getTime() -
              new Date(a.requestDate).getTime()
          );
          this.isLoading = false;
        },
        (error: any) => {
          this.alertMessage =
            error.error.responseData[1] || 'Some error occured.';
          this.alertTitle = error.error.responseData[0] || 'Error';
          this.showAlert = true;
          this.isLoading = false;
        }
      );
  }

 public filterRequests(status: string): void {
  this.activeFilter = status;

  if (status === 'All') {
    this.filteredRequests = [...this.requests];
  } else {
    const selected = this.requests.filter(
      (req: any) => req.requestStatus === status
    );
    const rest = this.requests.filter(
      (req: any) => req.requestStatus !== status
    );
    this.filteredRequests = [...selected, ...rest];
  }
}


  public openAssignAssetModal(request: any): void {
    this.requests = request;
    this.showAssignAssetModal = true;
    this.assignAssetForm.reset(); // Reset the form when modal opens
  }

  public closeAssignAssetModal(): void {
    this.showAssignAssetModal = false;
  }

  public openViewModal(request: any): void {
    this.selectedRequest = request;
    if (request.requestStatus === 'Accepted') {
      this.showAssignAssetModal = true;
      this.getAssetsByCategory();
      this.assignAssetForm.reset();
      // call get asset catagory apy to get teh catagories then populate them in the catagory dropdown in fullfill wala popup
      // also get availabe assets
    } else {
      this.showViewModal = true;
    }
  }

  public closeViewModal(): void {
    this.showViewModal = false;
  }

  public acceptFromModal(requestId: number): void {
    this.showAssignAssetModal = true;
    this.assetRequestManagementService
      .UpdateAssetRequestStatusToAccepted(requestId)
      .subscribe(
        (response) => {
          this.alertMessage =
            response.responseData[1] || 'Operation comepletred.';
          this.alertTitle = response.responseData[0] || 'Success';
          this.showAlert = true;
          this.isLoading = false;
        },
        (error) => {
          this.alertMessage =
            error.error.responseData[1] || 'Some error occured.';
          this.alertTitle = error.error.responseData[0] || 'Error';
          this.showAlert = true;
          this.isLoading = false;
        },
        () => {
          this.getallAssetrequest();
          this.filterRequests(this.activeFilter);
          this.showViewModal = false;
          this.selectedRequest = this.requests.filter(
            (x) => x.requestId === this.selectedRequest.requestId
          )[0];
          this.showViewModal = false;
          this.showAssignAssetModal = true;
        }
      );
  }

  public rejectFromModal(requestId: number): void {
    this.isLoading = true;
    this.assetRequestManagementService
      .UpdateAssetRequestStatusToDeclined(requestId)
      .subscribe(
        (response: any) => {
          this.alertMessage =
            response.responseData[1] || 'Request Declined Successfully';
          this.alertTitle = response.responseData[0] || 'Success';
          this.showAlert = true;
          this.isLoading = false;
          this.showViewModal = false;
          this.showAssignAssetModal = false;
        },
        (error: any) => {
          this.alertMessage =
            error.error.responseData[1] || 'Some error occured.';
          this.alertTitle = error.error.responseData[0] || 'Error';
          this.showAlert = true;
          this.isLoading = false;
        },
        () => {
          this.getallAssetrequest();
          this.filterRequests(this.activeFilter);
          this.selectedRequest = this.requests.filter(
            (x) => x.requestId === this.selectedRequest.requestId
          )[0];
        }
      );
  }

  public countByFilter(filterType: string): number {
    return this.requests.filter((x) => x.requestStatus === filterType).length;
  }

  public getAssetsByCategory(): void {
    this.isLoading = true;
    this.assetManagementService
      .GetAllAssetCatagory(this.organizationId)
      .subscribe(
        (response: any) => {
          console.log('Category data:', response);
          this.availableAssetsCategory = response.responseData || [];
          this.isLoading = false;
        },
        (error: any) => {
          this.alertMessage =
            error.error.responseData?.[1] || 'Failed to load assets.';
          this.alertTitle = error.error.responseData?.[0] || 'Error';
          this.showAlert = true;
          this.isLoading = false;
        }
      );
  }

  public getAvailableAsstes(event: any): void {
    this.availableAssets;
    console.log(event.target.value);
    // call the api to get available AssetsComponent.
  }

  onSubmit() {
    this.isLoading = true;
  if (this.assignAssetForm.valid) {
    this.closeAssignAssetModal(); // Close modal first (optional)

    this.assetRequestManagementService
      .UpdateAssetRequestStatusToFulfilled(this.selectedRequest)
      .subscribe(
        (response) => {
          this.alertMessage = response.responseData[1] || 'Request fulfilled successfully';
          this.alertTitle = response.responseData[0] || 'Success';
          this.isLoading = false;

        },
        (error) => {
          this.alertTitle = error.error.responseData[0];
          this.alertMessage = error.error.responseData[1];
          this.showAlert = true;
          this.isLoading = false;
        },
        ()=>{
            this.getallAssetrequest();
        }
      );
  } else {
    this.assignAssetForm.markAllAsTouched();
  }
}

}
