import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AssetRequestManagementService } from '../../../../core/services/AssetRequestManagement/asset-request-management.service';
import { NewAssetRequest } from '../../../../core/models/newAssetRequest';

@Component({
  selector: 'app-my-asset-requests',
  templateUrl: './my-asset-requests.component.html',
  styleUrl: './my-asset-requests.component.css',
})
export class MyAssetRequestsComponent implements OnInit {
  // Injecting the AssetRequestManagementService to handle asset requests
  private assetRequestManagementService: AssetRequestManagementService = inject(
    AssetRequestManagementService
  );
  private organizationId: number;
  // Modal form for new asset request
  public showNewAssetRequestForm: boolean;
  public newAssetRequestForm: FormGroup;
  public showFormError: boolean;

  // Asset requests data
  public activeFilter: string;
  public dashboardData: any;
  public originalRequests:any;
  public filteredRequests: Array<any>;
  // Assets data
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;

  constructor() {
    this.activeFilter = 'All';
    this.showNewAssetRequestForm = false;
    this.showFormError = false;

    this.newAssetRequestForm = new FormGroup({
      subject: new FormControl('', [Validators.required]),
      notes: new FormControl('', [Validators.required]),
    });

    this.showNewAssetRequestForm = false;
    this.dashboardData = {};
    this.originalRequests = {};
    this.organizationId =
      Number(sessionStorage.getItem('targetOrganizationID')) || 0;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';

    this.filteredRequests = [];
  }

  ngOnInit(): void {
    // Could load saved asset requests from storage here
    this.getallAssetrequest();
  }

  public getallAssetrequest(): void {
    this.isLoading = true;
    this.showAlert = false;
    this.assetRequestManagementService
      .GetAllAssetRequestEmployeeListStatsAndDesignatedAssets(
        this.organizationId
      )
      .subscribe(
        (response: any) => {
        // Sort requests by requestDate
        this.dashboardData = response.responseData;
        const sortedRequests = response.responseData.requiredAssetRequests.sort(
          (a: any, b: any) => new Date(b.requestDate).getTime() - new Date(a.requestDate).getTime()
        );

        // Set both main and original copies
        this.dashboardData.requiredAssetRequests = [...sortedRequests];
        this.dashboardData['originalRequests'] = [...sortedRequests];

        this.isLoading = false;
      },

        (error: any) => {
          this.alertMessage = error.error.responseData[1];
          this.alertTitle = error.error.responseData[0];
          this.showAlert = true;
          this.isLoading = false;
        }
      );
  }

  public openCloseNewAssetRequestForm(): void {
    this.newAssetRequestForm.reset();
    this.showFormError = false;
    this.showNewAssetRequestForm = !this.showNewAssetRequestForm;
  }

 public filterRequests(status: string): void {
  this.activeFilter = status;

  const allRequests = this.dashboardData?.originalRequests || [];

  if (status === 'All') {
    this.dashboardData.requiredAssetRequests = [...allRequests];
  } else {
    const selected = allRequests.filter((req: any) => req.requestStatus === status);
    const rest = allRequests.filter((req: any) => req.requestStatus !== status);
    this.dashboardData.requiredAssetRequests = [...selected, ...rest];
  }
}



  public cancelRequest(request: number): void {
    this.isLoading = true;
    //request.status = 'Cancel'; // or 'Cancelled', depending on your app
    this.assetRequestManagementService
      .UpdateAssetRequestStatusToCanceled(request)
      .subscribe(
        (response: any) => {
          this.alertMessage =
            response.responseData[1] || 'Request Cancel Successfully';
          this.alertTitle = response.responseData[0] || 'Success';
          this.showAlert = true;
          this.isLoading = false;
        },
        (error: any) => {
          this.alertMessage = error.error.responseData[1];
          this.alertTitle = error.error.responseData[0];
          this.showAlert = true;
          this.isLoading = false;
        },
        () => {
          this.getallAssetrequest();
        }
      );
  }

  public createNewRequest() {
    this.showNewAssetRequestForm = true;
  }

  public onSubmit() {
    this.isLoading = true;
    if (this.newAssetRequestForm.valid) {
      this.showNewAssetRequestForm = false;
      const NewAssetReq: NewAssetRequest = {
        organizationsAssetRequestID: 7,
        title: this.newAssetRequestForm.controls['subject'].value,
        description: this.newAssetRequestForm.controls['notes'].value,
        organizationID: this.organizationId,
      };
      this.newAssetRequestForm.reset();
      this.assetRequestManagementService
        .CreateAssetRequest(NewAssetReq)
        .subscribe(
          (response) => {
            this.alertMessage =
              response.responseData[1] || 'Request created Successfully';
            this.alertTitle = response.responseData[0] || 'Success';
            this.showAlert = true;
            this.isLoading = false;
          },
          (error) => {
            this.alertTitle = error.error.responseData[0];
            this.alertMessage = error.error.responseData[1];
            this.showAlert = true;
            this.isLoading = false;
          },
          () => {
            this.getallAssetrequest();
          }
        );
    } else {
      this.newAssetRequestForm.markAllAsTouched();
      this.isLoading = false;
    }
  }
}
