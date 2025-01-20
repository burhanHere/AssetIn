import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationEmployeeRoutingModule } from './organization-employee-routing.module';
import { OrganizationEmployeeBoardComponent } from './organization-employee-board/organization-employee-board.component';
import { RouterLink } from '@angular/router';
import { NevbarComponent } from '../../shared/components/nevbar/nevbar.component';
import { TopbarComponent } from '../../shared/components/topbar/topbar.component';
import { MyAssetRequestsComponent } from './my-asset-requests/my-asset-requests.component';


@NgModule({
  declarations: [
    OrganizationEmployeeBoardComponent,
    MyAssetRequestsComponent
  ],
  imports: [
    NevbarComponent,
    TopbarComponent,
    CommonModule,
    OrganizationEmployeeRoutingModule
  ]
})
export class OrganizationEmployeeModule { }
