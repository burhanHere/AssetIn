import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { JwtService } from '../../../core/services/jwt/jwt.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { OrganizationManagementService } from '../../../core/services/organizationManagement/organization-management.service';
import { HttpErrorResponse } from '@angular/common/http';
import { domainSuffixValidator } from '../../../shared/validators/domain-siffix.validator';
import { Organization } from '../../../core/models/organization';
import { ForgetPassword } from '../../../core/models/forget-password';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { ApiResponse } from '../../../core/models/apiResponse';
import { UserManagementService } from '../../../core/services/UserManagement/user-management.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {
  @ViewChild('imageUploadUser') imageUploadUser!: ElementRef<HTMLInputElement>;
  @ViewChild('imageUploadOrganization') imageUploadOrganization!: ElementRef<HTMLInputElement>;
  public profilePictureUser: string;
  public profilePictureOrganization: string;
  public selectedFile: File | null;
  private jwtService: JwtService = inject(JwtService);
  public userInfoForm: FormGroup;
  public organizationInfoForm: FormGroup;
  public isEditModeUser: boolean;
  public isEditModeOrganization: boolean;
  public userInfo: any;
  public products: any[];
  public isLoading: boolean;
  public showAlert: boolean;
  public alertTitle: string;
  public alertMessage: string;
  private target: string;
  public currentUserRole: string;
  public currentUserEmail: string;
  private organizationManagementService: OrganizationManagementService = inject(OrganizationManagementService);
  public organizationId: number;
  private organizationInfo: any;
  private authenticationService: AuthenticationService = inject(AuthenticationService);
  private userManagementService: UserManagementService = inject(UserManagementService);

  constructor() {
    this.userInfoForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      userEmail: new FormControl('', [Validators.required, Validators.email]),
      role: new FormControl('', [Validators.required]),
    });
    this.organizationInfoForm = new FormGroup({
      organizationName: new FormControl('', [Validators.required]),
      organizationDescription: new FormControl('', [Validators.required]),
      organizationDomain: new FormControl('', [Validators.required, domainSuffixValidator()]),
    });
    this.isEditModeUser = false;
    this.isEditModeOrganization = false;
    this.userInfo = {};
    this.products = [];
    this.isLoading = false;
    this.showAlert = false;
    this.alertTitle = '';
    this.alertMessage = '';
    this.profilePictureUser = '';
    this.profilePictureOrganization = '';
    this.selectedFile = null;
    this.target = '';
    const tempJwt = sessionStorage.getItem('auth-jwt') || '';
    let claims;
    if (tempJwt) {
      claims = this.jwtService.getTokenClaims(tempJwt);
    }
    this.currentUserRole = claims?.Role || '';
    this.currentUserEmail = claims?.Email || '';
    this.organizationId = Number(sessionStorage.getItem('targetOrganizationID')) || 0;
    this.organizationInfo = {};
  }

  ngOnInit() {
    // call user detail get api
    this.getUserInfo();
    if (this.currentUserRole === 'OrganizationOwner' && this.organizationId !== 0) {
      this.getOrganizationInfo();
    }
  }

  private getUserInfo(): void {
    this.isLoading = true;
    this.userManagementService.GetUserInfo().subscribe(
      (response: ApiResponse) => {
        this.userInfo = response.responseData;
        this.userInfoForm.controls['userName'].setValue(this.userInfo.userName);
        this.userInfoForm.controls['userEmail'].setValue(this.userInfo.email);
        this.userInfoForm.controls['role'].setValue(this.userInfo.roleName);
        this.profilePictureUser = this.userInfo.profilePicturePath || '';
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      }
    );
  }

  private getOrganizationInfo(): void {
    this.isLoading = true;
    this.organizationManagementService.GetOrganizationInfo(this.organizationId).subscribe(
      (response: any) => {
        this.organizationInfo = response.responseData;
        this.organizationInfoForm.controls['organizationName'].setValue(this.organizationInfo.organizationName);
        this.organizationInfoForm.controls['organizationDescription'].setValue(this.organizationInfo.description);
        this.organizationInfoForm.controls['organizationDomain'].setValue(this.organizationInfo.organizationDomain);
        this.profilePictureOrganization = this.organizationInfo.organizationLogo || '';
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      });
  }

  public resetPassword() {
    // Logic to reset user password
    const forgetPasswordData: ForgetPassword = {
      email: this.currentUserEmail
    };
    this.isLoading = true;
    this.authenticationService.ForgetPassword(forgetPasswordData).subscribe(
      (response: ApiResponse) => {
        this.alertTitle = 'Successful 🎉';
        this.alertMessage = response.responseData[0];
        this.showAlert = true;
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.alertTitle = error.status.toString();
        this.alertMessage = error.error.responseData[0];
        this.showAlert = true;
        this.isLoading = false;
      }
    );
  }

  public updateOrganizationInfo() {
    if (this.organizationInfoForm.valid) {
      this.isLoading = true;
      const organizationData: Organization = {
        organzationId: this.organizationId,
        organizationName: this.organizationInfoForm.controls['organizationName'].value,
        description: this.organizationInfoForm.controls['organizationDescription'].value,
        organizationDomain: this.organizationInfoForm.controls['organizationDomain'].value
      };
      this.organizationManagementService.UpdateOrganization(organizationData).subscribe(
        (response: ApiResponse) => {
          this.alertTitle = 'Success';
          this.alertMessage = 'Organization information updated successfully.';
          this.showAlert = true;
          this.isEditModeOrganization = false;
          this.isLoading = false;
          this.organizationInfoForm.markAsPristine();
        },
        (error: HttpErrorResponse) => {
          this.isLoading = false;
          this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
          this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
          this.showAlert = true;
        }, () => {
          this.getOrganizationInfo(); // Refresh organization info
        }
      );
    } else {
      this.organizationInfoForm.markAllAsTouched();
    }
  }



  public toggleEdit(target: string) {
    if (target === "User") {
      this.isEditModeUser = !this.isEditModeUser;
    } else if (target === "Organization") {
      this.isEditModeOrganization = !this.isEditModeOrganization;
    }
  }

  public cancelChanges(target: string) {
    // Revert to original data
    if (target === "User") {
      this.getUserInfo();
      this.isEditModeUser = false;
    } else if (target === "Organization") {
      this.organizationInfoForm.reset();
      this.getOrganizationInfo();
      this.isEditModeOrganization = false;
    }
  }

  public onDeleteImage(target: string): void {
    this.selectedFile = null;
    if (target === "User" && this.imageUploadUser) {
      this.imageUploadUser.nativeElement.value = '';
    } else if (target === "Organization" && this.imageUploadOrganization) {
      this.imageUploadOrganization.nativeElement.value = '';
    }
  }

  public onUploadImage(target: string) {
    this.target = target;
    if (target === "User") {
      this.imageUploadUser.nativeElement.click();
    }
    else if (target === "Organization") {
      this.imageUploadOrganization.nativeElement.click();
    }
  }

  public handleFileInput(event: Event): void {
    this.onFileSelected(event).subscribe({
      next: (file) => {
        this.selectedFile = file;
        this.uploadToServer(); // Only called after file is read and valid
      },
      error: (err) => {
        this.alertTitle = 'Error';
        this.alertMessage = err;
        this.showAlert = true;
        this.onDeleteImage(this.target);
      }
    });
  }

  private uploadToServer(): void {
    if (this.selectedFile) {
      this.isLoading = true;
      const formData = new FormData();
      if (this.target === "User") {
        formData.append('file', this.selectedFile, this.selectedFile.name);
        // call user profile picture update api
        this.userManagementService.UpdateUserProfilePicture(formData).subscribe(
          (response: ApiResponse) => {
            this.alertTitle = response.responseData?.[0];
            this.alertMessage = response.responseData?.[1];
            this.showAlert = true;
            this.isLoading = false;
            this.getUserInfo(); // Refresh user info
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
            this.isLoading = false;
            this.showAlert = true;
          }
        );
      } else if (this.target === "Organization") {
        // call organization profile picture update api    
        this.organizationManagementService.UploadOrganizationProfilePicture(this.selectedFile, this.organizationId).subscribe(
          (response: ApiResponse) => {
            this.alertTitle = response.responseData?.[0];
            this.alertMessage = response.responseData?.[1];
            this.showAlert = true;
            this.isLoading = false;
            this.getOrganizationInfo(); // Refresh organization info
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
            this.isLoading = false;
            this.showAlert = true;
          }
        );
      }
    } else {
      this.alertTitle = 'Error';
      this.alertMessage = 'No file selected for upload.';
      this.showAlert = true;
    }
  }

  private onFileSelected(event: Event): Observable<File> {
    return new Observable<File>((observer) => {
      const input = event.target as HTMLInputElement;

      if (!input.files || input.files.length === 0) {
        observer.error('No file selected.');
        return;
      }

      const file = input.files[0];

      // Validate type
      if (!file.type.startsWith('image/')) {
        observer.error('Please select a valid image file (jpg, jpeg, png, gif).');
        return;
      }

      // Validate size
      if (file.size > 5 * 1024 * 1024) {
        observer.error('File size must be less than 5MB.');
        return;
      }

      // FileReader for preview
      const reader = new FileReader();
      reader.onload = () => {
        if (this.target === "User") {
          this.profilePictureUser = reader.result as string;
        } else if (this.target === "Organization") {
          this.profilePictureOrganization = reader.result as string;
        }
        observer.next(file);
        observer.complete();
      };
      reader.onerror = () => {
        observer.error('Error reading file.');
      };

      reader.readAsDataURL(file);
    });
  }
}
