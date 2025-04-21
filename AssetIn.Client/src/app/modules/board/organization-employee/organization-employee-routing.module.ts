import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAssetRequestsComponent } from './my-asset-requests/my-asset-requests.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'myAssetRequests',
    pathMatch: 'full'
  },
  {
    path: 'myAssetRequests',
    component: MyAssetRequestsComponent,
    title: 'My Asset Requests'
  },
  {
    path: 'settings',
    component: SettingsComponent,
    title: 'Settings'
  },
  { path: '**', component: ErrorPageComponent, title: 'Dashboard' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationEmployeeRoutingModule { }
