import { PathLocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-my-asset-requests',
  templateUrl: './my-asset-requests.component.html',
  styleUrl: './my-asset-requests.component.css',
})
export class MyAssetRequestsComponent implements OnInit {
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

  constructor() {
    this.activeFilter = 'All';
    this.showNewAssetRequestForm = false;
    this.showFormError = false;

    this.newAssetRequestForm = new FormGroup({
      subject: new FormControl('', [Validators.required]),
      notes: new FormControl('', [Validators.required]),
    });

    this.requests = [
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Laptop Upgrade Request',
        description:
          'Employee needs a new laptop with better specs for graphic design tasks.',
        date: new Date('2024-12-12'),
        status: 'Pending',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'External Monitor Request',
        description:
          'Request for a second monitor for improved multitasking in financial analysis.',
        date: new Date('2024-12-12'),
        status: 'Pending',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Keyboard Replacement',
        description:
          'Employee’s keyboard is malfunctioning, causing typing issues.',
        date: new Date('2024-12-12'),
        status: 'Declined',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Noise-Canceling Headphones Request',
        description:
          'Employee requests noise-canceling headphones to improve focus in a noisy environment.',
        date: new Date('2024-12-12'),
        status: 'Fulfilled',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Noise-Canceling Headphones Request',
        description:
          'Employee requests noise-canceling headphones to improve focus in a noisy environment.',
        date: new Date('2024-12-12'),
        status: 'Fulfilled',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Noise-Canceling Headphones Request',
        description:
          'Employee requests noise-canceling headphones to improve focus in a noisy environment.',
        date: new Date('2024-12-12'),
        status: 'Fulfilled',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Noise-Canceling Headphones Request',
        description:
          'Employee requests noise-canceling headphones to improve focus in a noisy environment.',
        date: new Date('2024-12-12'),
        status: 'Fulfilled',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Noise-Canceling Headphones Request',
        description:
          'Employee requests noise-canceling headphones to improve focus in a noisy environment.',
        date: new Date('2024-12-12'),
        status: 'Pending',
      },
      {
        requestId: 488488,
        requisitionerId: 4352352,
        title: 'Noise-Canceling Headphones Request',
        description:
          'Employee requests noise-canceling headphones to improve focus in a noisy environment.',
        date: new Date('2024-12-12'),
        status: 'Cancel',
      },
      // Add more rows as needed...
    ];
    this.assets = [
      {
        name: 'Logitech K380s',
        category: 'Computer Accessory',
        imageUrl: '',
      },
      {
        name: 'MacBook Pro 2017',
        category: 'Laptop',
        imageUrl: '',
      },
      {
        name: 'Logitech K380s',
        category: 'Computer Accessory',
        imageUrl: '',
      },
      {
        name: 'Logitech K380s',
        category: 'Computer Accessory',
        imageUrl: '',
      },
      {
        name: 'Logitech K380s',
        category: 'Computer Accessory',
        imageUrl: '',
      },
    ];

    this.filteredRequests = [...this.requests];
  }

  ngOnInit(): void {
    // Could load saved asset requests from storage here
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

  public cancelRequest(request: any): void {
    console.log('Canceling request:', request);

    request.status = 'Cancel'; // or 'Cancelled', depending on your app
  }

  public createNewRequest(): void {
    console.log('New request initiated');
    this.openCloseNewAssetRequestForm();
  }
}
