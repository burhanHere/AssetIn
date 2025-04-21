import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationAdminRoutingModule } from './organization-admin-routing.module';
import { OrganizationAdminDashboardComponent } from './organization-admin-dashboard/organization-admin-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { AssetsComponent } from './assets/assets.component';
import { EmployeesComponent } from './employees/employees.component';
import { AssetRequestsComponent } from './asset-requests/asset-requests.component';
import { VendorsComponent } from './vendors/vendors.component';
import { SettingsComponent } from './settings/settings.component';

@NgModule({
  declarations: [
    OrganizationAdminDashboardComponent,
    AssetsComponent,
    EmployeesComponent,
    AssetRequestsComponent,
    VendorsComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule,
    OrganizationAdminRoutingModule,
    ErrorPageComponent
  ]
})
export class OrganizationAdminModule { }
