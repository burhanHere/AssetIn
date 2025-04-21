import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationOwnerRoutingModule } from './organization-owner-routing.module';
import { OrganizationsDashboardComponent } from './organizations-dashboard/organizations-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';


@NgModule({
  declarations: [
    OrganizationsDashboardComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule,
    OrganizationOwnerRoutingModule,
    ErrorPageComponent
  ]
})
export class OrganizationOwnerModule { }
