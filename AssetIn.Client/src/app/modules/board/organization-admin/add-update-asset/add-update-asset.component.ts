import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { Form, FormControl, FormGroup, Validators } from '@angular/forms';
import { BarController } from 'chart.js';
import { AssetManagementService } from '../../../../core/services/AssetManagement/asset-management.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-update-asset',
  templateUrl: './add-update-asset.component.html',
  styleUrl: './add-update-asset.component.css'
})
export class AddUpdateAssetComponent implements OnInit {
  @ViewChild('imageUpload') imageUpload!: ElementRef<HTMLInputElement>;
  private assetManagementService: AssetManagementService = inject(AssetManagementService);
  private organizationId: number;
  public selectedFile: File | null;
  public assetForm: FormGroup;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public assetCategories: any[];
  public assetTypes: any[];
  public uploadedAssetImage: string;
  public showNewAssetCatagoryForm: boolean;
  public newAssetCategoryOrTypeForm: FormGroup;
  public showPictureError: boolean;

  constructor() {
    const temp = sessionStorage.getItem('targetOrganizationID');
    this.organizationId = Number(temp === null || temp === undefined ? 0 : temp);
    this.assetForm = new FormGroup({
      assetName: new FormControl('', [Validators.required]),
      assetCategory: new FormControl('', [Validators.required]),
      serialNumber: new FormControl('', [Validators.required]),
      purchasePrice: new FormControl('', [Validators.required]),
      model: new FormControl('', [Validators.required]),
      manufacturer: new FormControl('', [Validators.required]),
      depreciationRate: new FormControl('', [Validators.required]),
      assetType: new FormControl('', [Validators.required]),
      purchaseDate: new FormControl('', [Validators.required]),
      location: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      problem: new FormControl('', []),
      costPrice: new FormControl('', [Validators.required]),
    });

    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.assetCategories = [];
    this.assetTypes = [];
    this.uploadedAssetImage = '';
    this.showNewAssetCatagoryForm = false;
    this.newAssetCategoryOrTypeForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      CatagoryOrType: new FormControl('', [Validators.required]),
    });
    this.selectedFile = null;
    this.showPictureError = false;
  }

  ngOnInit(): void {
    this.GetAssetCategories();
    this.GetAssetTypes();
  }

  private GetAssetCategories(): void {
    this.isLoading = true;
    this.assetManagementService.GetAllAssetCatagory(this.organizationId).subscribe(
      (response: any) => {
        this.assetCategories = response.responseData;
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.alertTitle = error.error?.responseData?.[0] || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
        this.showAlert = true;
        this.isLoading = false;
      }
    );
  }

  private GetAssetTypes(): void {
    this.isLoading = true;
    this.assetManagementService.GetAllAssetType(this.organizationId).subscribe(
      (response: any) => {
        this.assetTypes = response.responseData;
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.alertTitle = error.error?.responseData?.[0] || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
        this.showAlert = true;
        this.isLoading = false;
      }
    );
  }

  public createNewAssetTypeOrCategory(): void {
    if (this.newAssetCategoryOrTypeForm.valid) {
      const requestType = this.newAssetCategoryOrTypeForm.controls["CatagoryOrType"].value
      this.isLoading = true;
      if (requestType == "Asset Catagory") {
        const apiInput = {
          "organizationsAssetCatagoryID": 0,
          "organizationsAssetCatagoryName": this.newAssetCategoryOrTypeForm.controls["name"].value,
          "organizationsID": this.organizationId
        }
        this.assetManagementService.CreateNewAssetCatagory(apiInput).subscribe(
          (response: any) => {
            this.alertTitle = response?.responseData?.[0] || 'Success';
            this.alertMessage = response?.responseData?.[1] || 'New asset category created successfully';
            this.showAlert = true;
            this.isLoading = false;
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
            this.showAlert = true;
            this.isLoading = false;
          }, () => {
            this.GetAssetCategories();
          }
        );
      } else {
        //requestType == Asset Type
        const apiInput = {
          "organizationsAssetTypeID": 0,
          "organizationsAssetTypeName": this.newAssetCategoryOrTypeForm.controls["name"].value,
          "organizationsID": this.organizationId
        }
        this.assetManagementService.CreateNewAssetType(apiInput).subscribe(
          (response: any) => {
            this.alertTitle = response?.responseData?.[0] || 'Success';
            this.alertMessage = response?.responseData?.[1] || 'New asset type created successfully';
            this.showAlert = true;
            this.isLoading = false;
            this.newAssetCategoryOrTypeForm.reset();
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
            this.showAlert = true;
            this.isLoading = false;
          }, () => {
            this.GetAssetTypes();
          }
        );
      }
      this.newAssetCategoryOrTypeForm.reset();
    } else {
      this.newAssetCategoryOrTypeForm.markAllAsTouched();
    }
  }

  public createNewAsset(): void {
    if (this.assetForm.valid && this.selectedFile) {
      this.isLoading = true;
      this.showPictureError = false;
      const apiInput = new FormData();

      // Map frontend form names to backend DTO property names (PascalCase)
      apiInput.append('AssetlD', '0');
      apiInput.append('AssetName', this.assetForm.controls["assetName"].value || '');
      apiInput.append('Description', this.assetForm.controls["description"].value || '');
      apiInput.append('SerialNumber', this.assetForm.controls["serialNumber"].value || '');
      apiInput.append('Model', this.assetForm.controls["model"].value || '');
      apiInput.append('Manufacturer', this.assetForm.controls["manufacturer"].value || '');
      apiInput.append('PurchaseDate', this.assetForm.controls["purchaseDate"].value || '');
      apiInput.append('PurchasePrice', this.assetForm.controls["purchasePrice"].value || '0');
      apiInput.append('CostPrice', this.assetForm.controls["costPrice"].value || '0');
      apiInput.append('Location', this.assetForm.controls["location"].value || '');
      apiInput.append('DepreciationRate', this.assetForm.controls["depreciationRate"].value || '0');
      apiInput.append('Problem', this.assetForm.controls["problem"].value || ' '); // Make sure this has a value
      apiInput.append('AssetCatagoryID', this.assetForm.controls["assetCategory"].value || '0');
      apiInput.append('AssetTypeID', this.assetForm.controls["assetType"].value || '0');
      apiInput.append('OrganizationID', this.organizationId.toString());

      // Add the image file if selected
      if (this.selectedFile) {
        apiInput.append('ProfilePicturePath', this.selectedFile, this.selectedFile.name);
      }
      this.assetManagementService.CreateAsset(apiInput).subscribe(
        (response: any) => {
          this.alertTitle = response?.responseData?.[0] || 'Success';
          this.alertMessage = response?.responseData?.[1] || 'New asset created successfully';
          this.showAlert = true;
          this.isLoading = false;
          this.assetForm.reset();
          this.uploadedAssetImage = '';
        },
        (error: HttpErrorResponse) => {
          this.alertTitle = error.error?.responseData?.[0] || 'Error';
          this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
          this.showAlert = true;
          this.isLoading = false;
        }
      );
    } else {
      this.showPictureError = true;
      this.assetForm.markAllAsTouched();
    }
  }

  public onReset(): void {
    this.assetForm.reset([
      { assetCategory: '', assetType: '' }
    ]);
  }

  onDeleteImage(): void {
    this.uploadedAssetImage = '';
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
      if (this.selectedFile) {
        this.showPictureError = false;
      }

      // Create preview
      const reader = new FileReader();
      reader.onload = (e) => {
        this.uploadedAssetImage = e.target?.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  public closeNewAssetCategoryOrTypeForm(): void {
    this.showNewAssetCatagoryForm = false;
    this.newAssetCategoryOrTypeForm.reset();
  }
}
