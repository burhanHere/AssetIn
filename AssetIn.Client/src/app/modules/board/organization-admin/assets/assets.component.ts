import { Component, inject, OnInit } from '@angular/core';
import { AssetManagementService } from '../../../../core/services/AssetManagement/asset-management.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-assets',
  templateUrl: './assets.component.html',
  styleUrl: './assets.component.css'
})
export class AssetsComponent implements OnInit {
  private AssetManagementService: AssetManagementService = inject(AssetManagementService);
  private organizationId: number;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public assetList: any;
  public showDeleteAssetAlert: boolean;

  constructor() {
    const temp = sessionStorage.getItem('targetOrganizationID');
    this.organizationId = Number(temp === null || temp === undefined ? 0 : temp);
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.assetList = null;
    this.showDeleteAssetAlert = false;
  }

  ngOnInit(): void {
    this.getAssetList();
  }

  private getAssetList(): void {
    this.isLoading = true;
    this.AssetManagementService.GetAllAsset(this.organizationId).subscribe(
      (responce: any) => {
        this.isLoading = false;
        this.assetList = responce.responseData;
      },
      (error: HttpErrorResponse) => {
        this.isLoading = false;
        this.showAlert = true;
        this.alertTitle = error.error.responseData[0] || 'Error';
        this.alertMessage = error.error.responseData[1] || 'An error occurred.';
      }
    );
  }

  public exportAssetList(): void {

  }

  viewAssetDetails(targetAssetId: number): void {
    alert('viewAssetDetails: ' + targetAssetId)
  }

  deleteAsset(targetAssetId: number): void {
    this.showDeleteAssetAlert = false;
    this.isLoading = true;
    this.AssetManagementService.DeleteAsset(targetAssetId).subscribe(
      (responce: any) => {
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {
        this.isLoading = false;
        this.showAlert = true;
        this.alertTitle = error.error.responseData[0] || 'Error';
        this.alertMessage = error.error.responseData[1] || 'An error occurred.';
      },
      () => {
        this.getAssetList();
      }
    );
  }
}
