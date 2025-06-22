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
  private assetManagementService: AssetManagementService = inject(AssetManagementService);
  private organizationId: number;
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
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
    if (this.assetForm.valid) {
      this.isLoading = true;
      const apiInput = {
        "assetlD": 0,
        "assetName": this.assetForm.controls["assetName"].value,
        "description": this.assetForm.controls["description"].value,
        "serialNumber": this.assetForm.controls["serialNumber"].value,
        "model": this.assetForm.controls["model"].value,
        "manufacturer": this.assetForm.controls["manufacturer"].value,
        "purchaseDate": this.assetForm.controls["purchaseDate"].value,
        "purchasePrice": this.assetForm.controls["purchasePrice"].value,
        "costPrice": this.assetForm.controls["costPrice"].value,
        "location": this.assetForm.controls["location"].value,
        "depreciationRate": this.assetForm.controls["depreciationRate"].value,
        "problem": this.assetForm.controls["problem"].value,
        "assetCatagoryID": this.assetForm.controls["assetCategory"].value,
        "assetTypeID": this.assetForm.controls["assetType"].value,
        "organizationID": this.organizationId,
        "profilePicturePath": ""
      };
      this.assetManagementService.CreateAsset(apiInput).subscribe(
        (response: any) => {
          this.alertTitle = response?.responseData?.[0] || 'Success';
          this.alertMessage = response?.responseData?.[1] || 'New asset created successfully';
          this.showAlert = true;
          this.isLoading = false;
          this.assetForm.reset();
        },
        (error: HttpErrorResponse) => {
          this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
          this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
          this.showAlert = true;
          this.isLoading = false;
        }
      );
    } else {
      this.assetForm.markAllAsTouched();
    }
  }

  public onReset(): void {
    console.log('Form reseted');
    this.assetForm.reset();
  }
}
