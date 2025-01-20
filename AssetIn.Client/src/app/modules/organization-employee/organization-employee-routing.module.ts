import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationEmployeeBoardComponent } from './organization-employee-board/organization-employee-board.component';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';
import { MyAssetRequestsComponent } from './my-asset-requests/my-asset-requests.component';

const routes: Routes = [
  { path: '', redirectTo: 'organizationEmployeeMain', pathMatch: 'full' },
  {
    path: 'organizationEmployeeMain',
    component: OrganizationEmployeeBoardComponent,
    children: [
      { path: '', redirectTo: 'myAssetRequests', pathMatch: 'full' },
      { path: 'myAssetRequests', component: MyAssetRequestsComponent, title: 'My Requests' },
      {
        path: '**',
        component: ErrorPageComponent,
        title: 'Error',
      }
    ],
  },
  {
    path: '**',
    component: ErrorPageComponent,
    title: 'Error',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationEmployeeRoutingModule { }
