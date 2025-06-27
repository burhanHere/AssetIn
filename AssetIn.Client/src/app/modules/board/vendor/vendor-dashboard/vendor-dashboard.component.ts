import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { VendorManagementService } from '../../../../core/services/VendorManagement/vendor-management.service';
import { HttpErrorResponse } from '@angular/common/http';
import { JwtService } from '../../../../core/services/jwt/jwt.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-vendor-dashboard',
  templateUrl: './vendor-dashboard.component.html',
  styleUrl: './vendor-dashboard.component.css',
})
export class VendorDashboardComponent {
  @ViewChild('imageUpload') imageUpload!: ElementRef<HTMLInputElement>;
  public profilePicture: string;
  public selectedFile: File | null;
  private vendorManagementService: VendorManagementService = inject(
    VendorManagementService
  );
  private jwtService: JwtService = inject(JwtService);
  public vendorInfoForm: FormGroup;
  public isEditMode: boolean;
  public vendor: any;
  public products: any[];
  public isLoading: boolean;
  public showAlert: boolean;
  public alertTitle: string;
  public alertMessage: string;

  constructor() {
    this.vendorInfoForm = new FormGroup({
      vendorName: new FormControl('', [Validators.required]),
      contactPersonName: new FormControl('', [Validators.required]),
      vendorEmail: new FormControl('', [Validators.required, Validators.email]),
      vendorPhone: new FormControl('', [
        Validators.required,
        Validators.maxLength(13),
        Validators.minLength(13),
        Validators.pattern(/^\+\d{1,3}\d{9,12}$/),
      ]),
      vendorAddress: new FormControl('', [Validators.required]),
    });
    this.isEditMode = false;
    this.vendor = {};
    this.products = [];
    this.isLoading = false;
    this.showAlert = false;
    this.alertTitle = '';
    this.alertMessage = '';
    this.profilePicture = '';
    this.selectedFile = null;
  }

  ngOnInit() {
    // call vendor detail get api
    this.getVendorInfo();
  }

  private getVendorInfo(): void {
    this.isLoading = true;
    this.vendorManagementService.GetVendorInfo().subscribe(
      (response: any) => {
        this.vendor = response.responseData;
        this.profilePicture = this.vendor?.profilePicturePath || '';
        if (this.vendor !== null) {
          this.setValidatorData();
          this.getVendorProducts();
        }
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.alertTitle = 'Note';
        this.alertMessage = 'Please comeplete you profile setup.';
        // this.alertTitle = error.error?.responseData?.[0] || 'Error';
        // this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      },
      () => {
        this.getVendorProducts();
      }
    );
  }

  private getVendorProducts(): void {
    this.isLoading = true;
    this.vendorManagementService.GetVendorProducts().subscribe(
      (response: any) => {
        this.products = response.responseData || [];
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        // this.alertTitle = error.error?.responseData?.[0] || 'Error';
        // this.alertMessage =
        //   error.error?.responseData?.[1] || 'Unknown error occurred';
        // this.showAlert = true;
        this.isLoading = false;
      }
    );
  }

  private setValidatorData() {
    this.vendorInfoForm.controls['vendorName'].setValue(this.vendor.vendorName);
    this.vendorInfoForm.controls['contactPersonName'].setValue(
      this.vendor.contactPerson
    );
    this.vendorInfoForm.controls['vendorEmail'].setValue(this.vendor.email);
    this.vendorInfoForm.controls['vendorPhone'].setValue(
      this.vendor.phoneNumber
    );
    this.vendorInfoForm.controls['vendorAddress'].setValue(
      this.vendor.officeAddress
    );
  }

  public toggleEdit() {
    this.isEditMode = !this.isEditMode;
  }

  public saveChanges() {
    if (this.vendorInfoForm.valid) {
      this.isLoading = true;
      const tempJwt = sessionStorage.getItem('auth-jwt') || '';
      let claims;
      if (tempJwt) {
        claims = this.jwtService.getTokenClaims(tempJwt);
      }
      const vendorData = {
        vendorID: 0,
        vendorName: this.vendorInfoForm.controls['vendorName'].value,
        officeAddress: this.vendorInfoForm.controls['vendorAddress'].value,
        phoneNumber: this.vendorInfoForm.controls['vendorPhone'].value,
        email: this.vendorInfoForm.controls['vendorEmail'].value,
        contactPerson: this.vendorInfoForm.controls['contactPersonName'].value,
        userID: claims?.userID || '0',
      };
      this.vendorManagementService.CreateUpdateVendorInfo(vendorData).subscribe(
        (response: any) => {
          this.alertTitle = response.responseData?.[0] || 'Success';
          this.alertMessage =
            response.responseData?.[1] || 'Vendor data updated successfully.';
          this.isEditMode = false;
          this.isLoading = false;
          this.showAlert = true;
        },
        (error: HttpErrorResponse) => {
          this.alertTitle = error.error?.responseData?.[0] || 'Error';
          this.alertMessage =
            error.error?.responseData?.[1] || 'Unknown error occurred';
          this.isLoading = false;
          this.showAlert = true;
        }
      );
    } else {
      this.vendorInfoForm.markAllAsTouched();
    }
  }

  public cancelChanges() {
    // Revert to original data
    this.isEditMode = false;
    this.vendorInfoForm.reset();
    this.setValidatorData();
  }

  public onDeleteImage(): void {
    this.profilePicture = '';
    this.selectedFile = null;
    if (this.imageUpload) {
      this.imageUpload.nativeElement.value = '';
    }
  }

  public onUploadImage() {
    this.imageUpload.nativeElement.click();
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
        this.onDeleteImage();
      },
    });
  }

  private uploadToServer(): void {
    if (this.selectedFile) {
      this.isLoading = true;
      const formData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);
      this.vendorManagementService
        .UploadVendorProfilePicture(formData)
        .subscribe(
          (response: any) => {
            this.alertTitle = response.responseData?.[0] || 'Success';
            this.alertMessage =
              response.responseData?.[1] ||
              'Vendor profile picture updated successfully.';
            this.isLoading = false;
            this.showAlert = true;
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || 'Error';
            this.alertMessage =
              error.error?.responseData?.[1] || 'Unknown error occurred';
            this.isLoading = false;
            this.showAlert = true;
          }
        );
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
        observer.error(
          'Please select a valid image file (jpg, jpeg, png, gif).'
        );
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
        this.profilePicture = reader.result as string;
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
