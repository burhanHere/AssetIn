import {
  Component,
  inject,
} from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PageLoaderComponent } from '../page-loader/page-loader.component';
import { AlertCardComponent } from '../alert-card/alert-card.component';
import { RouteChangeDetectionService } from '../../../core/services/RouteChangeDetection/route-change-detection.service';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [PageLoaderComponent, AlertCardComponent, CommonModule, FormsModule],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.css',
})
export class TopbarComponent {
  private router: Router = inject(Router);
  private routeChangeDetectionService: RouteChangeDetectionService = inject(
    RouteChangeDetectionService
  );

  // Modal and UI state
  public showOverlay: boolean = false;
  public showSearchBar: boolean = false;
  public isGenerating: boolean = false;

  // Report state
  public selectedReportType: string = '';
  public reportData: any[] = [];
  public tableHeaders: string[] = [];

  // Filter dropdown data
  public assetTypes: any[] = [];
  public assetCategories: any[] = [];
  public employees: any[] = [];

  // Filter selections
  public filters = {
    assetStatus: '',
    assetType: '',
    assetCategory: '',
    assignedTo: '',
    employeeRole: '',
    employeeStatus: '',
    specificEmployee: '',
    gender: '',
    requestStatus: '',
    requestedBy: '',
    dateFrom: '',
    dateTo: '',
    priority: '',
    completionStatus: '',
    globalDateFrom: '',
    globalDateTo: ''
  };

  // Organization ID (get from session storage)
  private get organizationId(): number {
    return parseInt(sessionStorage.getItem('targetOrganizationID') || '0');
  }

  // Legacy properties (keeping for backward compatibility)
  public targetReport: string = '';
  private tableHeadersAndPropertyEmployee: any[] = [];
  private tableHeadersAndPropertyAsset: any[] = [];
  private tableHeadersAndPropertyAssetRequest: any[] = [];
  public tableHeadersAndProperty: any[] = [];
  public tableBody: any[] = [];

  constructor() {
    this.initializeTableHeaders();
    this.routeChangeDetectionService.routeChanged.subscribe(() => {
      this.showSearchBar = this.router.url.includes(
        '/board/mainBoard/organizationAdmin'
      );
    });
  }

  private initializeTableHeaders(): void {
    this.tableHeadersAndPropertyEmployee = [
      { headerText: 'Id', propertyName: 'Id' },
      { headerText: 'Full Name', propertyName: 'FullName' },
      { headerText: 'User Name', propertyName: 'UserName' },
      { headerText: 'Email', propertyName: 'Email' },
      { headerText: 'Phone Number', propertyName: 'PhoneNumber' }
    ];
  }

  // Report type change handler
  public onReportTypeChange(): void {
    this.resetFilters();
    this.loadFilterData();
    this.setTableHeaders();
  }
  // Load filter dropdown data based on report type
  private loadFilterData(): void {
    if (!this.organizationId) {
      console.error('Organization ID not found loadFilterData()');
      return;
    }

    switch (this.selectedReportType) {
      case 'Assets':
        this.loadAssetFilterData();
        break;
      case 'Employee':
        this.loadEmployeeFilterData();
        break;
      case 'Assets Request':
        this.loadRequestFilterData();
        break;
    }
  }

  private loadAssetFilterData(): void {
    // Load asset types
    // this.crystalReportsService.getAssetTypes(this.organizationId).subscribe({
    //   next: (data) => this.assetTypes = data,
    //   error: (error) => {
    //     console.error('Error loading asset types:', error);
    //     // Fallback to mock data
    //     this.assetTypes = [
    //       { id: 1, name: 'Laptop' },
    //       { id: 2, name: 'Desktop' },
    //       { id: 3, name: 'Printer' },
    //       { id: 4, name: 'Phone' }
    //     ];
    //   }
    // });

    // // Load asset categories
    // this.crystalReportsService.getAssetCategories(this.organizationId).subscribe({
    //   next: (data) => this.assetCategories = data,
    //   error: (error) => {
    //     console.error('Error loading asset categories:', error);
    //     // Fallback to mock data
    //     this.assetCategories = [
    //       { id: 1, name: 'IT Equipment' },
    //       { id: 2, name: 'Office Equipment' },
    //       { id: 3, name: 'Furniture' }
    //     ];
    //   }
    // });

    // this.loadEmployees();
  }

  private loadEmployeeFilterData(): void {
    // this.loadEmployees();
  }

  private loadRequestFilterData(): void {
    // this.loadEmployees();
  }

  private loadEmployees(): void {
    // this.crystalReportsService.getEmployees(this.organizationId).subscribe({
    //   next: (data) => this.employees = data,
    //   error: (error) => {
    //     console.error('Error loading employees:', error);
    //     // Fallback to mock data
    //     this.employees = [
    //       { id: '1', name: 'John Doe', email: 'john@company.com' },
    //       { id: '2', name: 'Jane Smith', email: 'jane@company.com' },
    //       { id: '3', name: 'Mike Johnson', email: 'mike@company.com' }
    //     ];
    //   }
    // });
  }

  // Set table headers based on report type
  private setTableHeaders(): void {
    switch (this.selectedReportType) {
      case 'Assets':
        this.tableHeaders = ['Asset ID', 'Asset Name', 'Status', 'Type', 'Category', 'Assigned To', 'Purchase Date'];
        break;
      case 'Employee':
        this.tableHeaders = ['Employee ID', 'Name', 'Email', 'Role', 'Status', 'Phone', 'Gender'];
        break;
      case 'Assets Request':
        this.tableHeaders = ['Request ID', 'Requested By', 'Asset Type', 'Status', 'Request Date', 'Priority', 'Completion Status'];
        break;
    }
  }
  // Generate report
  public generateReport(): void {
    if (!this.organizationId) {
      alert('Organization ID not found. Please ensure you are logged in properly.');
      return;
    }

    this.isGenerating = true;

    // const reportRequest: ReportRequest = {
    //   reportType: this.selectedReportType,
    //   filters: this.filters,
    //   organizationId: this.organizationId
    // };

    // this.crystalReportsService.generateCrystalReport(reportRequest).subscribe({
    //   next: (blob) => {
    //     this.isGenerating = false;
    //     const fileName = `${this.selectedReportType}_Report_${new Date().toISOString().split('T')[0]}.pdf`;
    //     this.crystalReportsService.downloadFile(blob, fileName);
    //     alert(`${this.selectedReportType} report generated successfully!`);
    //   },
    //   error: (error) => {
    //     this.isGenerating = false;
    //     console.error('Error generating report:', error);
    //     alert('Error generating report. Please try again.');
    //   }
    // });
  }

  // Preview report data
  public previewReport(): void {
    if (!this.organizationId) {
      alert('Organization ID not found. Please ensure you are logged in properly.');
      return;
    }

    this.isGenerating = true;

    // const reportRequest: ReportRequest = {
    //   reportType: this.selectedReportType,
    //   filters: this.filters,
    //   organizationId: this.organizationId
    // };

    // this.crystalReportsService.getReportPreview(reportRequest).subscribe({
    //   next: (response) => {
    //     this.reportData = response.data;
    //     this.isGenerating = false;
    //   },
    //   error: (error) => {
    //     this.isGenerating = false;
    //     console.error('Error loading preview:', error);
    //     // Fallback to mock data for demonstration
    //     this.generateMockData();
    //   }
    // });
  }

  // Generate mock data for preview
  private generateMockData(): void {
    switch (this.selectedReportType) {
      case 'Assets':
        this.reportData = [
          { 'Asset ID': 'AST001', 'Asset Name': 'Dell Laptop', 'Status': 'Assigned', 'Type': 'Laptop', 'Category': 'IT Equipment', 'Assigned To': 'John Doe', 'Purchase Date': '2024-01-15' },
          { 'Asset ID': 'AST002', 'Asset Name': 'HP Printer', 'Status': 'Available', 'Type': 'Printer', 'Category': 'Office Equipment', 'Assigned To': '', 'Purchase Date': '2024-02-20' }
        ];
        break;
      case 'Employee':
        this.reportData = [
          { 'Employee ID': 'EMP001', 'Name': 'John Doe', 'Email': 'john@company.com', 'Role': 'Employee', 'Status': 'Active', 'Phone': '123-456-7890', 'Gender': 'Male' },
          { 'Employee ID': 'EMP002', 'Name': 'Jane Smith', 'Email': 'jane@company.com', 'Role': 'Asset Manager', 'Status': 'Active', 'Phone': '123-456-7891', 'Gender': 'Female' }
        ];
        break;
      case 'Assets Request':
        this.reportData = [
          { 'Request ID': 'REQ001', 'Requested By': 'John Doe', 'Asset Type': 'Laptop', 'Status': 'Pending', 'Request Date': '2024-06-01', 'Priority': 'High', 'Completion Status': 'Pending' },
          { 'Request ID': 'REQ002', 'Requested By': 'Jane Smith', 'Asset Type': 'Printer', 'Status': 'Fulfilled', 'Request Date': '2024-05-28', 'Priority': 'Medium', 'Completion Status': 'Completed' }
        ];
        break;
    }
  }

  // Get field value for table display
  public getFieldValue(row: any, header: string): string {
    return row[header] || '-';
  }

  // Quick date range selection
  public setQuickDateRange(event: any): void {
    const days = parseInt(event.target.value);
    if (days) {
      const today = new Date();
      const fromDate = new Date(today.getTime() - (days * 24 * 60 * 60 * 1000));

      this.filters.globalDateFrom = fromDate.toISOString().split('T')[0];
      this.filters.globalDateTo = today.toISOString().split('T')[0];
    }
  }

  // Reset all filters
  public resetFilters(): void {
    this.filters = {
      assetStatus: '',
      assetType: '',
      assetCategory: '',
      assignedTo: '',
      employeeRole: '',
      employeeStatus: '',
      specificEmployee: '',
      gender: '',
      requestStatus: '',
      requestedBy: '',
      dateFrom: '',
      dateTo: '',
      priority: '',
      completionStatus: '',
      globalDateFrom: '',
      globalDateTo: ''
    };
    this.reportData = [];
  }
  // Export functions
  public exportToPDF(): void {
    if (!this.organizationId) {
      alert('Organization ID not found. Please ensure you are logged in properly.');
      return;
    }

    // const reportRequest: ReportRequest = {
    //   reportType: this.selectedReportType,
    //   filters: this.filters,
    //   organizationId: this.organizationId
    // };

    // this.crystalReportsService.exportToPDF(reportRequest).subscribe({
    //   next: (blob) => {
    //     const fileName = `${this.selectedReportType}_Report_${new Date().toISOString().split('T')[0]}.pdf`;
    //     this.crystalReportsService.downloadFile(blob, fileName);
    //   },
    //   error: (error) => {
    //     console.error('Error exporting to PDF:', error);
    //     alert('Error exporting to PDF. Please try again.');
    //   }
    // });
  }

  public exportToExcel(): void {
    if (!this.organizationId) {
      alert('Organization ID not found. Please ensure you are logged in properly.');
      return;
    }

    // const reportRequest: ReportRequest = {
    //   reportType: this.selectedReportType,
    //   filters: this.filters,
    //   organizationId: this.organizationId
    // };

    // this.crystalReportsService.exportToExcel(reportRequest).subscribe({
    //   next: (blob) => {
    //     const fileName = `${this.selectedReportType}_Report_${new Date().toISOString().split('T')[0]}.xlsx`;
    //     this.crystalReportsService.downloadFile(blob, fileName);
    //   },
    //   error: (error) => {
    //     console.error('Error exporting to Excel:', error);
    //     alert('Error exporting to Excel. Please try again.');
    //   }
    // });
  }

  // Legacy methods (keeping for backward compatibility)
  public getTargetData(event: Event): void {
    this.targetReport = (event.target as HTMLSelectElement).value;
    this.selectedReportType = this.targetReport;
    this.onReportTypeChange();

    if (this.targetReport === 'Assets') {
      this.getAssets()
    } else if (this.targetReport === 'Employee') {
      this.getEmployees()
    } else if (this.targetReport === 'Assets Request') {
      this.getAssetRequest();
    }
  }

  private getEmployees(): void {
    // Legacy method - functionality moved to loadEmployeeFilterData
  }

  private getAssetRequest(): void {
    // Legacy method - functionality moved to loadRequestFilterData
  }

  private getAssets(): void {
    // Legacy method - functionality moved to loadAssetFilterData
  }

  public LogoutUser(): void {
    sessionStorage.removeItem('auth-jwt');
    sessionStorage.removeItem('targetOrganizationID');
    this.router.navigateByUrl('auth');
  }
}
