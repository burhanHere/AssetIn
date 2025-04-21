import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationsDashboardComponent } from './organizations-dashboard/organizations-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'organizationsDashboard',
    pathMatch: 'full'
  },
  {
    path: 'organizationsDashboard',
    component: OrganizationsDashboardComponent,
    title: 'Dashboard'
  },
  {
    path: 'organizationsSettings',
    component: SettingsComponent,
    title: 'Settings'
  },
  { path: '**', component: ErrorPageComponent, title: 'Dashboard' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationOwnerRoutingModule { }
