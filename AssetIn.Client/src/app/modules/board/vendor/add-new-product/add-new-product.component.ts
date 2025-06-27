import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component, ElementRef, inject, input, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VendorManagementService } from '../../../../core/services/VendorManagement/vendor-management.service';

@Component({
  selector: 'app-add-new-product',
  templateUrl: './add-new-product.component.html',
  styleUrl: './add-new-product.component.css'
})
export class AddNewProductComponent {
  @ViewChild('imageUpload') imageUpload!: ElementRef<HTMLInputElement>;
  public profilePicture: string;
  public selectedFile: File | null;
  private vendorManagementService: VendorManagementService = inject(VendorManagementService);
  private router: Router = inject(Router);
  public productForm: FormGroup;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertTitle: string;
  public alertMessage: string;


  constructor() {
    this.productForm = new FormGroup({
      productName: new FormControl('', [Validators.required]),
      unitPrice: new FormControl('', [Validators.required]),
      model: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
    });
    this.isLoading = false;
    this.showAlert = false;
    this.alertTitle = '';
    this.alertMessage = '';
    this.profilePicture = '';
    this.selectedFile = null;
  }

  onSubmit() {
    const formData = new FormData();

    // Add form fields
    formData.append('ProductName', this.productForm.get('productName')?.value || '');
    formData.append('Model', this.productForm.get('model')?.value || '');
    formData.append('Price', this.productForm.get('unitPrice')?.value || '0');
    formData.append('Description', this.productForm.get('description')?.value || '');
    formData.append('VendorID', '1'); // Replace with actual vendor ID

    // Use this.selectedFile for the ProfilePicture property
    if (this.selectedFile) {
      formData.append('ProfilePicture', this.selectedFile, this.selectedFile.name);
    }
    this.vendorManagementService.CreateVendorProduct(formData).subscribe(
      (response: any) => {
        this.alertTitle = response.responseData?.[0] || 'Success';
        this.alertMessage = response.error?.responseData?.[1] || 'Product Created Successfully';
        this.isLoading = false;
        this.showAlert = true;
        this.productForm.reset();
      }, (error: HttpErrorResponse) => {
        this.alertTitle = error.error?.responseData?.[0] || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      });

  }


  onCancel() {
    this.productForm.reset();
    this.router.navigate(['/board/mainBoard/vendor/vendorDashboard']);
  }

  onDeleteImage(): void {
    this.profilePicture = '';
    this.selectedFile = null;
    if (this.imageUpload) {
      this.imageUpload.nativeElement.value = '';
    }
  }

  onUploadImage() {
    this.imageUpload.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];

      // Validate file type
      if (!file.type.startsWith('image/')) {
        this.alertTitle = 'Error';
        this.alertMessage = 'Please select a valid image file (jpg, jpeg, png, gif).';
        this.showAlert = true;
        return;
      }

      // Validate file size (max 5MB)
      if (file.size > 5 * 1024 * 1024) {
        this.alertTitle = 'Error';
        this.alertMessage = 'File size must be less than 5MB.';
        this.showAlert = true;
        return;
      }

      // Store the file for API upload
      this.selectedFile = file;

      // Create preview
      const reader = new FileReader();
      reader.onload = (e) => {
        this.profilePicture = e.target?.result as string;
      };
      reader.readAsDataURL(file);
    }
  }
}
