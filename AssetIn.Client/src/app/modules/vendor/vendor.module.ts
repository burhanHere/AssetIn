import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorRoutingModule } from './vendor-routing.module';
import { VendorBoardComponent } from './vendor-board/vendor-board.component';
import { TopbarComponent } from '../../shared/components/topbar/topbar.component';
import { NevbarComponent } from '../../shared/components/nevbar/nevbar.component';
import { RouterLink } from '@angular/router';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';


@NgModule({
  declarations: [VendorBoardComponent, VendorDashboardComponent],
  imports: [
    CommonModule,
    VendorRoutingModule,
    TopbarComponent,
    NevbarComponent,
  ]
})
export class VendorModule { }
