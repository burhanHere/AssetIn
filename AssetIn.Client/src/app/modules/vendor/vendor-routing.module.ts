import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';
import { VendorBoardComponent } from './vendor-board/vendor-board.component';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: 'vendorMain', pathMatch: 'full' },
  {
    path: 'vendorMain',
    component: VendorBoardComponent,

    children: [
      { path: '', redirectTo: 'vendorDashboard', pathMatch: 'full' },
      { path: 'vendorDashboard', component: VendorDashboardComponent, title: 'Dashboard' },
      {
        path: '**',
        component: ErrorPageComponent,
        title: 'Error',
      }
    ]
  },
  {
    path: '**',
    component: ErrorPageComponent,
    title: 'Error',
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorRoutingModule { }
