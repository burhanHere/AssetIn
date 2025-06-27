import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssetManagementService } from '../../../../core/services/AssetManagement/asset-management.service';
import { checkIn } from '../../../../core/models/checkIn';
import { checkOut } from '../../../../core/models/checkOut';
import { retireAsset } from '../../../../core/models/retireAsset';
import {
  FormGroup,
  Validators,
  FormControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { JwtService } from '../../../../core/services/jwt/jwt.service';
import { EmployeeManagementService } from '../../../../core/services/EmployeeManagement/employee-management.service';
import { sendToMaintenance } from '../../../../core/models/sendToMaintenance';
import { returnFromMaintenance } from '../../../../core/models/returnFromMaintenance';

@Component({
  selector: 'app-asset-details',
  templateUrl: './asset-details.component.html',
  styleUrl: './asset-details.component.css',
})
export class AssetDetailsComponent implements OnInit {
  private router: Router = inject(Router);
  private activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  private assetManagementService: AssetManagementService = inject(
    AssetManagementService
  );
  private employeeManagementService: EmployeeManagementService = inject(
    EmployeeManagementService
  );

  private jwtservice: JwtService = inject(JwtService);
  public assetId: number;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public asset: any;
  private organizationId: number;
  public checkInForm: FormGroup;
  public showCheckInModal: boolean;
  public employees: any[];
  public showCheckOutModal: boolean;
  public checkOutForm: FormGroup;
  public showSendToMaintenanceModal: boolean;
  public sendToMaintenanceForm: FormGroup;
  public showReturnFromMaintenanceModal: boolean;
  public returnFromMaintenanceForm: FormGroup;
  public showDeleteAssetAlert: boolean;

  constructor() {
    this.organizationId =
      Number(sessionStorage.getItem('targetOrganizationID')) || 0;
    this.assetId = 0;
    this.activatedRoute.queryParamMap.subscribe((params) => {
      this.assetId = Number(params.get('assetId'));
    });
    this.checkInForm = new FormGroup({
      notes: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
      ]),
    });

    this.checkOutForm = new FormGroup({
      assignedToUserId: new FormControl('', Validators.required),
      notes: new FormControl('', Validators.required),
    });
    this.sendToMaintenanceForm = new FormGroup({
      problem: new FormControl('', Validators.required),
    });
    this.returnFromMaintenanceForm = new FormGroup({
      maintanenceResult: new FormControl('', Validators.required),
      isRepaired: new FormControl(false),
    });
    this.showCheckOutModal = false;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.asset = {};
    this.showCheckInModal = false;
    this.employees = [];
    this.showSendToMaintenanceModal = false;
    this.showReturnFromMaintenanceModal = false;
    this.showDeleteAssetAlert = false;
  }

  ngOnInit(): void {
    this.getAssetDetail();
  }

  public getAssetDetail(): void {
    this.isLoading = true;
    this.assetManagementService.GetAsset(this.assetId).subscribe(
      (response) => {
        this.asset = response.responseData;
        this.isLoading = false;
      },
      (error) => {
        this.alertTitle =
          error.error?.responseData?.[0] || 'Error';
        this.alertMessage =
          error.error?.responseData?.[1] ||
          error.error?.message ||
          'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      }
    );
  }

  public getallEmployees(): void {
    this.isLoading = true;
    this.showAlert = false;
    this.employeeManagementService
      .GetEmployeeList(this.organizationId)
      .subscribe(
        (response) => {
          this.employees = response.responseData;
          this.isLoading = false;
        },
        (error) => {
          this.alertTitle =
            error.error?.responseData?.[0] || 'Error';
          this.alertMessage =
            error.error?.responseData?.[1] ||
            error.error?.message ||
            'Unknown error occurred';
          this.showAlert = true;
          this.isLoading = false;
        }
      );
  }

  public showDeleteAssetConfirmation() {
    this.showDeleteAssetAlert = !this.showDeleteAssetAlert;
  }

  public deleteAsset(): void {
    this.isLoading = true;
    this.assetManagementService.DeleteAsset(this.assetId).subscribe(
      (responce: any) => {
        this.alertTitle = responce.responseData?.[0] || 'Success';
        this.alertMessage =
          responce.responseData?.[1] || 'Asset Deleted Successfully';
        this.isLoading = false;
        this.showAlert = true;
        this.router.navigate([
          '/board/mainBoard/organizationAdmin/organizationAssets',
        ]);
      },
      (error) => {
        this.alertTitle =
          error.error?.responseData?.[0] || 'Error';
        this.alertMessage =
          error.error?.responseData?.[1] ||
          error.error?.message ||
          'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      }
    );
  }

  public checkIn(): void {
    const tempJwt = sessionStorage.getItem('auth-jwt') || '';
    let claims;
    if (tempJwt) {
      claims = this.jwtservice.getTokenClaims(tempJwt);
    }
    const checkInData: checkIn = {
      assetId: this.assetId,
      checkInByUserId: claims?.userId || '',
      organizationID: this.organizationId,
      checkInDate: new Date(),
      notes: this.checkInForm.value.notes,
    };
    this.assetManagementService.CheckInAsset(checkInData).subscribe(
      (response) => {
        this.alertTitle = 'Success';
        this.alertMessage = 'Asset checked in successfully.';
        this.showAlert = true;
        this.closeCheckInModal();
      },
      (error) => {
        this.alertTitle =
          error.error?.responseData?.[0] || 'Error';
        this.alertMessage =
          error.error?.responseData?.[1] ||
          error.error?.message ||
          'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      },
      () => {
        this.getAssetDetail();
      }
    );
  }

  public openCheckInModal(): void {
    this.showCheckInModal = true;
  }

  public closeCheckInModal(): void {
    this.showCheckInModal = false;
    this.checkInForm.reset();
  }

  public checkOut(): void {
    this.isLoading = true;
    const tempJwt = sessionStorage.getItem('auth-jwt') || '';
    let claims;
    if (tempJwt) {
      claims = this.jwtservice.getTokenClaims(tempJwt);
    }
    const checkOutData: checkOut = {
      assetId: this.assetId,
      assignedToUserId: this.checkOutForm.controls['assignedToUserId'].value,
      assignedByUserId: claims?.UserId || '',
      organizationID: this.organizationId,
      checkOutDate: new Date(),
      notes: this.checkOutForm.value.notes,
    };

    this.assetManagementService.CheckOutAsset(checkOutData).subscribe(
      (response) => {
        this.alertTitle = 'Success';
        this.alertMessage = 'Asset checked out successfully.';
        this.showAlert = true;
        this.closeCheckOutModal();
      },
      (error) => {
        this.alertTitle =
          error.error?.responseData?.[0] || 'Error';
        this.alertMessage =
          error.error?.responseData?.[1] ||
          error.error?.message ||
          'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      },
      () => {
        this.getAssetDetail();
      }
    );
  }

  openCheckOutModal() {
    this.getallEmployees();
    this.showCheckOutModal = true;
  }

  closeCheckOutModal() {
    this.showCheckOutModal = false;
    this.checkOutForm.reset();
  }

  public retire(): void {
    const retireData: retireAsset = {
      retirementReason: 'Retirement reason here',
      retirementDate: new Date(),
      condition: 'Retired',
      assetID: this.assetId,
      organizationID: this.organizationId,
    };
    this.assetManagementService.RetireAsset(retireData).subscribe(
      (response) => {
        this.isLoading = false;
        this.alertTitle = 'Success';
        this.alertMessage = 'Asset retired successfully.';
        this.showAlert = true;
        console.log('Retirement successful:', response);
      },
      (error) => {
        this.alertTitle =
          error.error?.responseData?.[0] || 'Error';
        this.alertMessage =
          error.error?.responseData?.[1] ||
          error.error?.message ||
          'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      },
      () => {
        this.getAssetDetail();
      }
    );
  }

  public updateAsset(): void {
    this.router.navigateByUrl(`/board/mainBoard/organizationAdmin/addUpdateAsset?assetId=${this.assetId}`);
  }

  public sendToMaintenance(): void {
    this.isLoading = true;
    const maintenanceData: sendToMaintenance = {
      problem: this.sendToMaintenanceForm.controls['problem'].value,
      assetID: this.assetId,
      organizationID: this.organizationId,
    };
    this.assetManagementService
      .SendAssetToMaintenance(maintenanceData)
      .subscribe(
        (response: any) => {
          this.alertTitle = response.responseData?.[0] || 'success';
          this.alertMessage =
            response.responseData?.[1] ||
            'Asset mask as under maintanence successfully';
          this.closeSendToMaintenanceModal();
          this.isLoading = false;
          this.showAlert = true;
        },
        (error) => {
          this.alertTitle =
            error.error?.responseData?.[0] || 'Error';
          this.alertMessage =
            error.error?.responseData?.[1] ||
            'Unknown error occurred';
          this.isLoading = false;
          this.showAlert = true;
        },
        () => {
          this.getAssetDetail();
        }
      );
  }

  public openSendToMaintenanceModal() {
    this.showSendToMaintenanceModal = true;
  }

  public closeSendToMaintenanceModal() {
    this.showSendToMaintenanceModal = false;
    this.sendToMaintenanceForm.reset();
  }

  public returnFromMaintenance(): void {
    if (this.returnFromMaintenanceForm.invalid) return;

    const returnData: returnFromMaintenance = {
      retunDate: new Date(),
      maintanenceResult:
        this.returnFromMaintenanceForm.controls['maintanenceResult'].value,
      isRepaired: this.returnFromMaintenanceForm.controls['isRepaired'].value,
      assetID: this.assetId,
      organizationID: this.organizationId,
    };

    this.assetManagementService
      .ReturnAssetFromMaintenance(returnData)
      .subscribe(
        (response) => {
          this.alertTitle = 'Success';
          this.alertMessage = 'Asset returned from maintenance successfully.';
          this.showAlert = true;
          this.closeReturnFromMaintenanceModal();
        },
        (error) => {
          this.alertTitle =
            error.error?.responseData?.[0] || 'Error';
          this.alertMessage =
            error.error?.responseData?.[1] ||
            'Unknown error occurred';
          this.isLoading = false;
          this.showAlert = true;
        },
        () => {
          this.getAssetDetail();
        }
      );
  }

  public openReturnFromMaintenanceModal() {
    this.showReturnFromMaintenanceModal = true;
  }

  public closeReturnFromMaintenanceModal() {
    this.showReturnFromMaintenanceModal = false;
    this.returnFromMaintenanceForm.reset();
  }
}
