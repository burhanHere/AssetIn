import { Component, inject, OnInit } from '@angular/core';
import { AssetManagementService } from '../../../../core/services/AssetManagement/asset-management.service';
import { HttpErrorResponse } from '@angular/common/http';
import { HelperFunctionService } from '../../../../core/services/HelperFunction/helper-function.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-assets',
  templateUrl: './assets.component.html',
  styleUrl: './assets.component.css'
})
export class AssetsComponent implements OnInit {
  private router: Router = inject(Router);
  private AssetManagementService: AssetManagementService = inject(AssetManagementService);
  private helperFunctionService: HelperFunctionService = inject(HelperFunctionService);
  private organizationId: number;
  private assetToDelete: any;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public assetList: any[];
  public showDeleteAssetAlert: boolean;

  constructor() {
    const temp = sessionStorage.getItem('targetOrganizationID');
    this.organizationId = Number(temp === null || temp === undefined ? 0 : temp);
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.assetList = [];
    this.showDeleteAssetAlert = false;
    this.assetToDelete = {};
  }

  ngOnInit(): void {
    this.getAssetList();
  }

  private getAssetList(): void {
    this.isLoading = true;
    this.AssetManagementService.GetAllAsset(this.organizationId).subscribe(
      (responce: any) => {

        this.isLoading = false;
        this.assetList = [];
        this.assetList = responce.responseData;
      },
      (error: HttpErrorResponse) => {

        this.isLoading = false;
        this.showAlert = true;
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
      }
    );
  }

  public downloadAssetList(): void {
    this.helperFunctionService.exportAssetList(this.assetList, 'asset-list.csv');
  }

  public viewAssetDetails(targetAssetId: number): void {
    alert('viewAssetDetails: ' + targetAssetId)
  }

  public showDeleteAssetConfirmation(targetAsset: any) {
    this.showDeleteAssetAlert = !this.showDeleteAssetAlert;
    this.assetToDelete = targetAsset;
  }

  public deleteAsset(): void {
    this.isLoading = true;
    this.showDeleteAssetAlert = false;
    this.AssetManagementService.DeleteAsset(this.assetToDelete.assetlD).subscribe(
      (responce: any) => {
        this.isLoading = false;
      },
      (error: HttpErrorResponse) => {

        this.isLoading = false;
        this.showAlert = true;
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
      },
      () => {
        this.getAssetList();
      }
    );
  }

  public NvgToAddUpdateAsset(): void {
    this.router.navigateByUrl('/board/mainBoard/organizationAdmin/addUpdateAsset');
  }

}
