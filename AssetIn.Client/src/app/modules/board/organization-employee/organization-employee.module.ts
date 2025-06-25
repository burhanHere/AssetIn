import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationEmployeeRoutingModule } from './organization-employee-routing.module';
import { MyAssetRequestsComponent } from './my-asset-requests/my-asset-requests.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AlertCardComponent } from '../../../shared/components/alert-card/alert-card.component';
import { PageLoaderComponent } from '../../../shared/components/page-loader/page-loader.component';


@NgModule({
  declarations: [
    MyAssetRequestsComponent,
  ],
  imports: [
    CommonModule,
    OrganizationEmployeeRoutingModule,
    ErrorPageComponent,
    ReactiveFormsModule,
    AlertCardComponent,
    PageLoaderComponent
  ]
})
export class OrganizationEmployeeModule { }
