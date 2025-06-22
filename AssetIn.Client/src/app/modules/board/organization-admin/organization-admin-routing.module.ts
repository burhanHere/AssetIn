import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationAdminDashboardComponent } from './organization-admin-dashboard/organization-admin-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { AssetsComponent } from './assets/assets.component';
import { EmployeesComponent } from './employees/employees.component';
import { AssetRequestsComponent } from './asset-requests/asset-requests.component';
import { VendorsComponent } from './vendors/vendors.component';
import { SettingsComponent } from './settings/settings.component';
import { AddUpdateAssetComponent } from './add-update-asset/add-update-asset.component';
import { ReportingComponent } from './reporting/reporting.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'organizationDashboard',
    pathMatch: 'full'
  },
  {
    path: 'organizationDashboard',
    component: OrganizationAdminDashboardComponent,
    title: 'Dashboard'
  },
  {
    path: 'organizationAssets',
    component: AssetsComponent,
    title: 'Assets'
  },
  {
    path: 'addUpdateAsset',
    component: AddUpdateAssetComponent,
    title: 'Add Update Asset'
  },
  {
    path: 'organizationEmployees',
    component: EmployeesComponent,
    title: 'Employees'
  },
  {
    path: 'organizationAssetsRequet',
    component: AssetRequestsComponent,
    title: 'Asset Requests'
  },
  {
    path: 'organizationVendors',
    component: VendorsComponent,
    title: 'Dashboard'
  },
  {
    path: 'organizationSettings',
    component: SettingsComponent,
    title: 'Settings'
  },
  {
    path: 'reporting',
    component: ReportingComponent,
    title: 'Reporting'
  },
  { path: '**', component: ErrorPageComponent, title: 'Dashboard' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrganizationAdminRoutingModule { }
