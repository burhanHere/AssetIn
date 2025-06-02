import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-asset-requests',
  templateUrl: './asset-requests.component.html',
  styleUrl: './asset-requests.component.css',
})
export class AssetRequestsComponent implements OnInit {
  public activeFilter: string;
  public requests: Array<any>;
  public selectedRequest:Array<any>;
  public filteredRequests: Array<any>;
  public assignAssetForm: FormGroup;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public showAssignAssetModal: boolean;

  constructor() {
    this.assignAssetForm = new FormGroup({
      category: new FormControl('', [Validators.required]),
      availableAssets: new FormControl('', [Validators.required]),
      notes: new FormControl(''),
    });
    this.activeFilter = 'All';
    this.requests = [
  {
    requestId: 1001,
    requisitionerId: 5001,
    title: 'Laptop Upgrade',
    description: 'Request for a high-performance laptop for development tasks.',
    date: new Date('2024-12-12'),
    status: 'Pending'
  },
  {
    requestId: 1002,
    requisitionerId: 5002,
    title: 'External Monitor',
    description: 'Need a second monitor for financial analysis.',
    date: new Date('2024-12-12'),
    status: 'Pending'
  },
  {
    requestId: 1003,
    requisitionerId: 5003,
    title: 'Ergonomic Chair',
    description: 'Need a comfortable chair for long working hours.',
    date: new Date('2024-12-12'),
    status: 'Fulfilled'
  },
  {
    requestId: 1004,
    requisitionerId: 5004,
    title: 'Tablet for Fieldwork',
    description: 'Require a 4G-enabled tablet for on-site data collection.',
    date: new Date('2024-12-12'),
    status: 'Fulfilled'
  },
  {
    requestId: 1005,
    requisitionerId: 5005,
    title: 'Keyboard Replacement',
    description: 'Current keyboard is not functioning properly.',
    date: new Date('2024-12-12'),
    status: 'Declined'
  },
   {
    requestId: 1004,
    requisitionerId: 5004,
    title: 'Tablet for Fieldwork',
    description: 'Require a 4G-enabled tablet for on-site data collection.',
    date: new Date('2024-12-12'),
    status: 'Fulfilled'
  },
  {
    requestId: 1005,
    requisitionerId: 5005,
    title: 'Keyboard Replacement',
    description: 'Current keyboard is not functioning properly.',
    date: new Date('2024-12-12'),
    status: 'Declined'
  }
];
    this.selectedRequest=[];
    this.filteredRequests = [...this.requests];
    this.showAssignAssetModal = false;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
  }

  availableAssets = [
    { id: 'asset1', name: 'Dell XPS 15' },
    { id: 'asset2', name: 'LG Ultrawide Monitor' },
  ];


  ngOnInit(): void {}
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

  public openAssignAssetModal(request: any): void {
    this.selectedRequest = request;
    this.showAssignAssetModal = true;
    this.assignAssetForm.reset(); // Reset the form when modal opens
  }

  public closeAssignAssetModal(): void {
    this.showAssignAssetModal = false;
  }

  public rejectRequest(request: any) {
    if (request.status === 'Pending') {
      request.status = 'Declined';
    }
  }

  public approveRequest(request: any) {
    this.showAssignAssetModal = true;
    request.status = 'Fulfilled';
    this.filterRequests(this.activeFilter);
  }

  onSubmit() {
    if (this.assignAssetForm.valid) {
      console.log(this.assignAssetForm.value);
       this.closeAssignAssetModal();
      // Handle request fulfillment here
    } else {
      this.assignAssetForm.markAllAsTouched();
    }
  }
}
