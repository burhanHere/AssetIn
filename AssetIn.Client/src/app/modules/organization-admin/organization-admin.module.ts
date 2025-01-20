import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationAdminRoutingModule } from './organization-admin-routing.module';
import { OrganizationAdminBoardComponent } from './organization-admin-board/organization-admin-board.component';
import { TopbarComponent } from "../../shared/components/topbar/topbar.component";
import { NevbarComponent } from "../../shared/components/nevbar/nevbar.component";
import { OrganizationAdminDashboardComponent } from './organization-admin-dashboard/organization-admin-dashboard.component';
import { OrganizationAdminAssetsComponent } from './organization-admin-assets/organization-admin-assets.component';
import { OrganizationAdminEmployeesComponent } from './organization-admin-employees/organization-admin-employees.component';
import { OrganizationAdminAssetRequestsComponent } from './organization-admin-asset-requests/organization-admin-asset-requests.component';


@NgModule({
  declarations: [
    OrganizationAdminBoardComponent,
    OrganizationAdminDashboardComponent,
    OrganizationAdminAssetsComponent,
    OrganizationAdminEmployeesComponent,
    OrganizationAdminAssetRequestsComponent
  ],
  imports: [
    CommonModule,
    OrganizationAdminRoutingModule,
    TopbarComponent,
    NevbarComponent,
]
})
export class OrganizationAdminModule { }
