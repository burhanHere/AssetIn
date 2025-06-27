import { Component, inject, OnInit } from '@angular/core';
import { OrganizationManagementService } from '../../../../core/services/organizationManagement/organization-management.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import {
  Chart,
  LineController,
  LineElement,
  PointElement,
  CategoryScale,
  LinearScale,
  Title,
  Tooltip,
  Filler,
  Legend,
  DoughnutController,
  ArcElement,
} from 'chart.js';
import { CrystalReportingService } from '../../../../core/services/crystalReporting/crystal-reporting.service';
import { HelperFunctionService } from '../../../../core/services/HelperFunction/helper-function.service';

Chart.register(
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  CategoryScale,
  Filler,
  Legend,
  Tooltip
);

@Component({
  selector: 'app-organization-admin-dashboard',
  templateUrl: './organization-admin-dashboard.component.html',
  styleUrl: './organization-admin-dashboard.component.css',
})
export class OrganizationAdminDashboardComponent implements OnInit {
  private router: Router = inject(Router);
  private organizationManagementService: OrganizationManagementService = inject(
    OrganizationManagementService
  );
  private crystalReportingService: CrystalReportingService = inject(CrystalReportingService);
  private helperFunctionService: HelperFunctionService = inject(HelperFunctionService);
  private organizationId: number;

  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public organizationData: any;
  public chart: any;

  constructor() {
    const temp = sessionStorage.getItem('targetOrganizationID');
    this.organizationId = Number(
      temp === null || temp === undefined ? 0 : temp
    );
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
    this.organizationData = null;
    // Register line chart components
    Chart.register(
      LineController,
      LineElement,
      PointElement,
      CategoryScale,
      LinearScale,
      DoughnutController,
      ArcElement,
      Title,
      Tooltip,
      Legend
    );
  }

  public fetchReportData(): void {
    this.isLoading = true;
    const reportFilterationData =
    {
      reportType: "Assets",
      //asset report
      assetType: 0,
      assetStatus: 0,
      assetCategory: 0,
      assignedTo: '',
      // employee report
      employeeRole: '',
      employeeStatus: false,
      specificEmployee: '',
      gender: '',
      // asset request report
      requestStatus: 0,
      requestedBy: '',
      // date filters
      fromDate: '',
      toDate: '',
      // target organization id
      organizationId: Number(this.organizationId)
    };
    this.crystalReportingService.GenerateHtmlReportByFilter(reportFilterationData).subscribe(
      (response: any) => {
        const reportData = response.responseData;
        const now = new Date();
        const formattedDate = `${(now.getMonth() + 1).toString().padStart(2, '0')}-${now.getDate().toString().padStart(2, '0')}-${now.getFullYear()}`;
        this.helperFunctionService.exportAssetList(reportData, `-report-${formattedDate}.csv`);
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

  ngOnInit(): void {
    if (this.organizationId === 0) {
      this.router.navigateByUrl('**');
    } else {
      this.isLoading = true;
      this.organizationManagementService
        .GetOrganizationInfoForOrganizationDashboard(this.organizationId)
        .subscribe(
          (responce: any) => {
            this.isLoading = false;
            this.organizationData = responce.responseData;
            // Initialize charts after data is received
            this.createChart(this.organizationData.chartsData);
            this.createDoughnutChart();
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || 'Unknown error occurred';
            this.isLoading = false;
            this.showAlert = true;
          }
        );
    }
  }

  public createChart(chartdata: any): void {
    const years = Object.keys(chartdata);
    this.chart = new Chart('MyChart', {
      type: 'line', // line chart

      data: {
        labels: [
          'Jan',
          'Feb',
          'Mar',
          'Apr',
          'May',
          'Jun',
          'Jul',
          'Aug',
          'Sep',
          'Oct',
          'Nov',
          'Dec',
        ],
        datasets: [
          {
            label: years[1],
            data: chartdata[years[1]],
            borderColor: '#f59e0b',
            tension: 0.3,
            fill: 'origin', // Fills the area under the line
            backgroundColor: 'rgba(233, 118, 11, 0.65)', // Add fill color with opacity
            pointRadius: 0, // <-- This enables hover detection
            pointHoverRadius: 20, // <-- Enlarges point on hover
          },
          {
            label: years[0],
            data: chartdata[years[0]],
            borderColor: '#8b5cf6',
            tension: 0.3,
            fill: 'origin', // Fills the area under the line
            backgroundColor: 'rgba(139, 92, 246, 0.4)', // Add fill color with opacity
            pointRadius: 0, // <-- This enables hover detection
            pointHoverRadius: 20, // <-- Enlarges point on hover
          },
        ],
      },

      options: {
        aspectRatio: 2.5,
        plugins: {
          legend: {
            display: true,
          },
        },
      },
    });
  }

  public createDoughnutChart(): void {
    this.chart = new Chart('MydoughnutChart', {
      type: 'doughnut',
      data: {
        labels: ['Fixed Assets', 'Variable Assets'],
        datasets: [
          {
            data: [
              this.organizationData?.organizationAssetRatioByAssetType[0]
                ?.assetRatioInType,
              this.organizationData?.organizationAssetRatioByAssetType[1]
                ?.assetRatioInType,
            ],
            backgroundColor: ['#D2468F', '#F6B84B'],
            borderWidth: 0,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        cutout: '70%',
        plugins: {
          legend: {
            display: false,
          },
          tooltip: {
            callbacks: {
              label: function (tooltipItem: any) {
                return tooltipItem.label + ': ' + tooltipItem.raw + '%';
              },
            },
          },
        },
      },
    });
  }

  public nvgToAddUpdateAsset(): void {
    this.router.navigateByUrl('/board/mainBoard/organizationAdmin/addUpdateAsset');
  }

  // not i use nay more
  // public exportOrganizationData(): void {
  //   if (!this.organizationData || typeof this.organizationData !== 'object') {
  //     this.alertTitle = 'No Data';
  //     this.alertMessage = 'No organization data available to export.';
  //     this.showAlert = true;
  //     return;
  //   }

  //   // Ensure data is in array form
  //   const dataArray = Array.isArray(this.organizationData)
  //     ? this.organizationData
  //     : [this.organizationData];

  //   // Get CSV headers from object keys
  //   const keys = Object.keys(dataArray[0]);
  //   const csvRows: string[] = [];

  //   // Add headers
  //   csvRows.push(keys.join(','));

  //   // Add rows
  //   for (const row of dataArray) {
  //     const values = keys.map((key) => {
  //       let val = row[key];
  //       if (val === null || val === undefined) val = '';
  //       if (typeof val === 'string') {
  //         val = val.replace(/"/g, '""'); // Escape double quotes
  //         return `"${val}"`;
  //       }
  //       return val;
  //     });
  //     csvRows.push(values.join(','));
  //   }

  //   // Create CSV blob and trigger download
  //   const csvContent = csvRows.join('\n');
  //   const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
  //   const url = window.URL.createObjectURL(blob);

  //   const link = document.createElement('a');
  //   link.href = url;
  //   link.setAttribute(
  //     'download',
  //     `organization_${this.organizationId}_data.csv`
  //   );
  //   document.body.appendChild(link);
  //   link.click();
  //   document.body.removeChild(link);
  //   window.URL.revokeObjectURL(url); // Clean up
  // }
}
