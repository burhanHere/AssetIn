import { PathLocationStrategy } from '@angular/common';
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
  // you did not injected the service
  private assetRequestManagementService:AssetRequestManagementService=inject(AssetRequestManagementService);
  private organizationId: number;
  // Modal form for new asset request
  public showNewAssetRequestForm: boolean;
  public newAssetRequestForm: FormGroup;
  public showFormError: boolean;

  // Asset requests data
  public activeFilter: string;
  public requests: Array<any>;
  public filteredRequests: Array<any>;
  // Assets data
  public assets: Array<any>;
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
    this.requests = [];
    this.assets = [];
    this.organizationId =
      Number(sessionStorage.getItem('targetOrganizationID')) || 0;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';

    this.filteredRequests = [...this.requests];
  }

  ngOnInit(): void {
    // Could load saved asset requests from storage here
    this.getallAssetrequest();
  }

  public getallAssetrequest():void{
    this.isLoading = true;
    this.showAlert = false;
    this.assetRequestManagementService.GetAllAssetRequestEmployeeListStatsAndDesignatedAssets(this.organizationId)
    .subscribe(
      (response: any) => {
          debugger;
          this.requests = response.responseData;
          this.isLoading = false;
          console.log('Request:', this.requests);
        },
        (error: any) => {
          debugger;
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

  // Filter requests by status
  public filterRequests(status: string): void {
    this.activeFilter = status;

    if (status === 'All') {
      this.filteredRequests = [...this.requests];
    } else {
      // Show selected status at top, others after
      const selected = this.requests.filter((req) => req.status === status);
      const rest = this.requests.filter((req) => req.status !== status);
      this.filteredRequests = [...selected, ...rest];
    }
  }

  public countRequests(status: string): number {
    if (status === 'All') {
      return this.requests.length;
    }
    return this.requests.filter((r) => r.status === status).length;
  }

  public cancelRequest(request: number): void {


    console.log('Canceling request:', request);

    //request.status = 'Cancel'; // or 'Cancelled', depending on your app
    this.assetRequestManagementService.UpdateAssetRequestStatusToCanceled(request).subscribe
  }


  public  createNewRequest() {
    this.showNewAssetRequestForm = true;
  }

  onSubmit() {
    if (this.newAssetRequestForm.valid) {
       this.showNewAssetRequestForm = false;
      const NewAssetReq : NewAssetRequest={
         organizationsAssetRequestID:7,
        title:this.newAssetRequestForm.controls['subject'].value,
        description:this.newAssetRequestForm.controls['notes'].value,
        organizationID: this.organizationId,
      }
      this.newAssetRequestForm.reset();
      this.assetRequestManagementService.CreateAssetRequest(NewAssetReq).subscribe(
        (response) => {
          debugger;
          this.isLoading = false;
        },
        (error) => {
          debugger;
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
      alert('Please Fill the form!!!');
      this.isLoading = false;
    }
  }
}
