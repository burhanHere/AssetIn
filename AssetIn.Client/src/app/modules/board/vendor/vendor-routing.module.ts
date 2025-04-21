import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'vendorDashboard',
    pathMatch: 'full'
  },
  {
    path: 'vendorDashboard',
    component: VendorDashboardComponent,
    title: 'Dashboard'
  },
  {
    path: 'vendorSettings',
    component: SettingsComponent,
    title: 'Settings'
  },
  { path: '**', component: ErrorPageComponent, title: 'Dashboard' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorRoutingModule { }
