import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorRoutingModule } from './vendor-routing.module';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';
import { AddNewProductComponent } from './add-new-product/add-new-product.component';
import { ReactiveFormsModule } from '@angular/forms';

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
    ReactiveFormsModule
  ]
})
export class VendorModule { }
