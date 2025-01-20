import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationOwnerBoardComponent } from './organization-owner-board/organization-owner-board.component';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';

import { OrganizationsDashboardComponent } from './organizations-dashboard/organizations-dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: 'organizationOwnerMain', pathMatch: 'full' },
  {
    path: 'organizationOwnerMain',
    component: OrganizationOwnerBoardComponent,
    children: [
      { path: '', redirectTo: 'organizationsDashboard', pathMatch: 'full' },
      {path:'organizationsDashboard',component:OrganizationsDashboardComponent,title:'Organizations Dashboard'},
      {
        path: '**',
        component: ErrorPageComponent,
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
export class OrganizationOwnerRoutingModule { }
