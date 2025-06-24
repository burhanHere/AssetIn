import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorRoutingModule } from './vendor-routing.module';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';
import { AddNewProductComponent } from './add-new-product/add-new-product.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PageLoaderComponent } from '../../../shared/components/page-loader/page-loader.component';
import { AlertCardComponent } from '../../../shared/components/alert-card/alert-card.component';

@NgModule({
  declarations: [
    VendorDashboardComponent,
    SettingsComponent,
    AddNewProductComponent
  ],
  imports: [
    CommonModule,
    VendorRoutingModule,
    ErrorPageComponent,
    ReactiveFormsModule,
    PageLoaderComponent,
    AlertCardComponent,
  ]
})
export class VendorModule { }
