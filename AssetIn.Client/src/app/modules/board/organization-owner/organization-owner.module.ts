import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationOwnerRoutingModule } from './organization-owner-routing.module';
import { OrganizationsDashboardComponent } from './organizations-dashboard/organizations-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PageLoaderComponent } from '../../../shared/components/page-loader/page-loader.component';
import { AlertCardComponent } from '../../../shared/components/alert-card/alert-card.component';


@NgModule({
  declarations: [
    OrganizationsDashboardComponent,
  ],
  imports: [
    CommonModule,
    OrganizationOwnerRoutingModule,
    ReactiveFormsModule,
    PageLoaderComponent,
    AlertCardComponent
  ]
})
export class OrganizationOwnerModule { }
