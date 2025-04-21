import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorRoutingModule } from './vendor-routing.module';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';

@NgModule({
  declarations: [
    VendorDashboardComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule,
    VendorRoutingModule,
    ErrorPageComponent
  ]
})
export class VendorModule { }
