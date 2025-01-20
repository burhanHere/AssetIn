import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationAdminBoardComponent } from './organization-admin-board/organization-admin-board.component';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';
import { OrganizationAdminDashboardComponent } from './organization-admin-dashboard/organization-admin-dashboard.component';
import { OrganizationAdminAssetsComponent } from './organization-admin-assets/organization-admin-assets.component';
import { OrganizationAdminEmployeesComponent } from './organization-admin-employees/organization-admin-employees.component';
import { OrganizationAdminAssetRequestsComponent } from './organization-admin-asset-requests/organization-admin-asset-requests.component';

const routes: Routes = [
  { path: '', redirectTo: 'organizationAdminMain', pathMatch: 'full' },
  {
    path: 'organizationAdminMain',
    component: OrganizationAdminBoardComponent,
    children: [
      { path: '', redirectTo: 'organizationAdminDashboard', pathMatch: 'full' },
      { path: 'organizationAdminDashboard', component: OrganizationAdminDashboardComponent, title: 'Dashboard' },
      { path: 'organizationAssets', component: OrganizationAdminAssetsComponent, title: 'Assets' },
      { path: 'organizationEmployees', component: OrganizationAdminEmployeesComponent, title: 'Employees' },
      { path: 'organizationAssetRequests', component: OrganizationAdminAssetRequestsComponent, title: 'Asset Requests' },
      {
        path: '**',
        component: ErrorPageComponent,
        title: 'Error',
      }
    ]
  },
  {
    path: '**',
    component: ErrorPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrganizationAdminRoutingModule { }
