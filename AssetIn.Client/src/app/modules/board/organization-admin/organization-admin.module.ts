import { inject, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationAdminRoutingModule } from './organization-admin-routing.module';
import { OrganizationAdminDashboardComponent } from './organization-admin-dashboard/organization-admin-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { AssetsComponent } from './assets/assets.component';
import { EmployeesComponent } from './employees/employees.component';
import { AssetRequestsComponent } from './asset-requests/asset-requests.component';
import { VendorsComponent } from './vendors/vendors.component';
import { PageLoaderComponent } from '../../../shared/components/page-loader/page-loader.component';
import { AlertCardComponent } from '../../../shared/components/alert-card/alert-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddUpdateAssetComponent } from './add-update-asset/add-update-asset.component';
import { ReportingComponent } from './reporting/reporting.component';
import { AssetDetailsComponent } from './asset-details/asset-details.component';

@NgModule({
  declarations: [
    OrganizationAdminDashboardComponent,
    AssetsComponent,
    EmployeesComponent,
    AssetRequestsComponent,
    VendorsComponent,
    AddUpdateAssetComponent,
    ReportingComponent,
    AssetDetailsComponent,
  ],
  imports: [
    CommonModule,
    OrganizationAdminRoutingModule,
    ErrorPageComponent,
    PageLoaderComponent,
    AlertCardComponent,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class OrganizationAdminModule { }
