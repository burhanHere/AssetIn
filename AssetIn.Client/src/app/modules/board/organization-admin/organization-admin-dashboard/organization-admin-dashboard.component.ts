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
  Legend,
  DoughnutController,
  ArcElement,
} from 'chart.js';

import { FormsModule } from '@angular/forms';

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

  ngOnInit(): void {
    if (this.organizationId === 0) {
      this.router.navigateByUrl('**');
    } else {

      this.isLoading = true;
      this.organizationManagementService
        .GetOrganizationInfoForOrganizationDashboard(this.organizationId)
        .subscribe(
          (responce: any) => {
            console.log(responce.responseData);// comment it or remove it after use
            this.isLoading = false;
            this.organizationData = responce.responseData;

            // Initialize charts after data is received
            this.createChart();
            this.createDoughnutChart();
          },
          (error: HttpErrorResponse) => {
            this.isLoading = false;
            this.showAlert = true;
            this.alertMessage = error.error.responseData[0] || 'Error';
            this.alertTitle =
              error.error.responseData[1] || 'An error occurred.';
          }
        );
    }
  }

  public createChart(): void {
    this.chart = new Chart('MyChart', {
      type: 'line', // Still a line chart

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
            label: '2024',
            data: [
              388.92, 0, 3695.19, 789.27, 398.07, 2911.98, 1371.88, 1367.44,
              1939.43, 400.37, 2893.25, 0,
            ],
            borderColor: '#f59e0b',
            tension: 0.3,
            fill: 'origin', // Fills the area under the line
            backgroundColor: 'rgba(245, 158, 11, 0.4)', // Add fill color with opacity
            pointRadius: 0,
          },
          {
            label: '2025',
            data: [
              384.36, 785.47, 974.4, 978.16, 1362.92, 2325.5, 380.57, 790.05,
              1552.67, 3308.77, 1940.39, 3102.47,
            ],
            borderColor: '#8b5cf6',
            tension: 0.3,
            fill: 'origin', // Fills the area under the line
            backgroundColor: 'rgba(139, 92, 246, 0.4)', // Add fill color with opacity
            pointRadius: 0,
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
            data: [100, 0],
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


  public exportOrganizationData(): void {
      if (!this.organizationData || typeof this.organizationData !== 'object') {
        this.alertTitle = 'No Data';
        this.alertMessage = 'No organization data available to export.';
        this.showAlert = true;
        return;
      }
    
      // Ensure data is in array form
      const dataArray = Array.isArray(this.organizationData)
        ? this.organizationData
        : [this.organizationData];
    
      // Get CSV headers from object keys
      const keys = Object.keys(dataArray[0]);
      const csvRows: string[] = [];
    
      // Add headers
      csvRows.push(keys.join(','));
    
      // Add rows
      for (const row of dataArray) {
        const values = keys.map(key => {
          let val = row[key];
          if (val === null || val === undefined) val = '';
          if (typeof val === 'string') {
            val = val.replace(/"/g, '""'); // Escape double quotes
            return `"${val}"`;
          }
          return val;
        });
        csvRows.push(values.join(','));
      }
    
      // Create CSV blob and trigger download
      const csvContent = csvRows.join('\n');
      const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
      const url = window.URL.createObjectURL(blob);
    
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', `organization_${this.organizationId}_data.csv`);
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      window.URL.revokeObjectURL(url); // Clean up
    }
    
  
}
