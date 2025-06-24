import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CrystalReportingService } from '../../../../core/services/crystalReporting/crystal-reporting.service';
import { HttpErrorResponse } from '@angular/common/http';
import { HelperFunctionService } from '../../../../core/services/HelperFunction/helper-function.service';

@Component({
  selector: 'app-reporting',
  templateUrl: './reporting.component.html',
  styleUrl: './reporting.component.css'
})
export class ReportingComponent implements OnInit {
  private crystalReportingService: CrystalReportingService = inject(CrystalReportingService);
  private helperFunctionService: HelperFunctionService = inject(HelperFunctionService);
  private organizationId: number;

  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;

  public selectedReportType: string;
  public assetTypes: any[];
  public assetStatuses: any[];
  public assetCatagories: any[];
  public employees: any[];
  public employeeStatus: any[];
  public employeeRoles: any[];
  public genders: any[];
  public assetRequestStatuses: any[];

  public reportingForm: FormGroup;

  private reportData: any;

  constructor() {
    const temp = sessionStorage.getItem('targetOrganizationID');
    this.organizationId = Number(temp === null || temp === undefined ? 0 : temp);

    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';

    this.assetTypes = [];
    this.assetStatuses = [];
    this.assetCatagories = [];
    this.employees = [];
    this.employeeRoles = [];
    this.employeeStatus = [];
    this.genders = [];
    this.assetRequestStatuses = [];
    this.selectedReportType = '';

    this.reportingForm = new FormGroup(
      {
        // Asset Report
        assetType: new FormControl(''),
        assetStatus: new FormControl(''),
        assetCategory: new FormControl(''),
        assignedTo: new FormControl(''),
        // Employee Report
        employeeRole: new FormControl(''),
        employeeStatus: new FormControl(''),
        specificEmployee: new FormControl(''),
        gender: new FormControl(''),
        // Asset Request Report
        requestStatus: new FormControl(''),
        requestedBy: new FormControl(''),

        // date filters
        fromDate: new FormControl(''),
        toDate: new FormControl(''),
        quickSelectDateRange: new FormControl(''),
      }
    );
    this.reportData = {};
  }

  ngOnInit(): void {
    this.GetFilterData();
  }

  private GetFilterData() {
    this.isLoading = true;
    this.crystalReportingService.GetFilterData(this.organizationId).subscribe(
      (response: any) => {
        this.assetRequestStatuses = response.responseData?.["assetRequestStatus"];
        this.genders = response.responseData?.["gender"];
        this.employeeRoles = response.responseData?.["employeeRoles"];
        this.employees = response.responseData?.["employees"];
        this.employeeStatus = response.responseData?.["employeeStatus"];
        this.assetCatagories = response.responseData?.["organizationAssetCategories"];
        this.assetStatuses = response.responseData?.["organizationAssetStatuses"];
        this.assetTypes = response.responseData?.["organizationAssetTypes"];
        this.isLoading = false;
      }, (error: HttpErrorResponse) => {
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.showAlert = true;
        this.isLoading = false;
      });
  }

  public onReportTypeChange(event: any): void {
    this.reportingForm.reset();
    this.selectedReportType = event.target.value;
  }

  public resetAllFilters(): void {
    this.reportingForm.reset();
  }

  public fetchReportData(): void {
    this.isLoading = true;
    const reportFilterationData =
    {
      reportType: this.selectedReportType,
      //asset report
      assetType: Number(this.reportingForm.controls['assetType'].value),
      assetStatus: Number(this.reportingForm.controls['assetStatus'].value),
      assetCategory: Number(this.reportingForm.controls['assetCategory'].value),
      assignedTo: this.reportingForm.controls['assignedTo'].value,
      // employee report
      employeeRole: this.reportingForm.controls['employeeRole'].value,
      employeeStatus: Boolean(this.reportingForm.controls['employeeStatus'].value),
      specificEmployee: this.reportingForm.controls['specificEmployee'].value,
      gender: this.reportingForm.controls['gender'].value,
      // asset request report
      requestStatus: Number(this.reportingForm.controls['requestStatus'].value),
      requestedBy: this.reportingForm.controls['requestedBy'].value,
      // date filters
      fromDate: this.reportingForm.controls['fromDate'].value,
      toDate: this.reportingForm.controls['toDate'].value,
      // target organization id
      organizationId: Number(this.organizationId)
    };

    this.crystalReportingService.GenerateHtmlReportByFilter(reportFilterationData).subscribe(
      (response: any) => {
        this.reportData = response.responseData;
        const now = new Date();
        const formattedDate = `${(now.getMonth() + 1).toString().padStart(2, '0')}-${now.getDate().toString().padStart(2, '0')}-${now.getFullYear()}`;
        this.helperFunctionService.exportAssetList(this.reportData, `${this.selectedReportType}-report-${formattedDate}.csv`);
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

  public updateDateRange(event: any): void {
    const selectedOption = event.target.value;
    const today: Date = new Date();
    let toDate: Date = today;
    let fromDate: Date = today;
    if (selectedOption === "") {
      this.reportingForm.controls['fromDate'].setValue('');
      this.reportingForm.controls['toDate'].setValue('');
    } else {
      if (selectedOption === "Last Year") {
        fromDate = new Date(today.getFullYear() - 1, today.getMonth(), today.getDate()); // Default to 1 year ago
      } else if (selectedOption === "Last 3 Months") {
        fromDate = new Date(today.getFullYear(), today.getMonth() - 3, today.getDate()); // Default to 3 months ago
      }
      else if (selectedOption === "Last 30 Days") {
        fromDate = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 30); // Default to 30 days ago
      }
      else if (selectedOption === "Last 7 Days") {
        fromDate = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 7); // Default to 7 days ago
      }
      this.reportingForm.controls['fromDate'].setValue(fromDate.toISOString().split('T')[0]);
      this.reportingForm.controls['toDate'].setValue(toDate.toISOString().split('T')[0]);
    }
  }
}
