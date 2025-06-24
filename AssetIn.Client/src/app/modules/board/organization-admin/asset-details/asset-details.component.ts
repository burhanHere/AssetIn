import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssetManagementService } from '../../../../core/services/AssetManagement/asset-management.service';

@Component({
  selector: 'app-asset-details',
  templateUrl: './asset-details.component.html',
  styleUrl: './asset-details.component.css',
})
export class AssetDetailsComponent implements OnInit {
  private activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  private assetManagementService: AssetManagementService = inject(
    AssetManagementService
  );
  public assetId: number;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public asset: any;
  constructor() {
    this.assetId = 0;
    this.activatedRoute.queryParamMap.subscribe((params) => {
      this.assetId = Number(params.get('assetId'));
    });
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.asset = {};
  }

  ngOnInit(): void {
    this.assetManagementService.GetAsset(this.assetId).subscribe(
      (response) => {
        this.isLoading = false;
        this.asset = response.responseData;
      },
      (error) => {
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.isLoading = false;
        this.showAlert = true;
      }
    );
  }


}
