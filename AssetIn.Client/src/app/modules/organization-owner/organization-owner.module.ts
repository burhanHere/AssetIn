import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationOwnerRoutingModule } from './organization-owner-routing.module';
import { OrganizationOwnerBoardComponent } from './organization-owner-board/organization-owner-board.component';
import { NevbarComponent } from "../../shared/components/nevbar/nevbar.component";
import { TopbarComponent } from "../../shared/components/topbar/topbar.component";
import { OrganizationsDashboardComponent } from './organizations-dashboard/organizations-dashboard.component';


@NgModule({
  declarations: [
    OrganizationOwnerBoardComponent,
    OrganizationsDashboardComponent
  ],
  imports: [
    CommonModule,
    OrganizationOwnerRoutingModule,
    NevbarComponent,
    TopbarComponent
]
})
export class OrganizationOwnerModule { }
