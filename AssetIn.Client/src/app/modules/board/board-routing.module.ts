import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { roleAuthGuard } from '../../core/guards/roleAuthGuard/role-auth.guard';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';
import { MainBoardComponent } from './main-board/main-board.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'mainBoard',
    pathMatch: 'full'
  },
  {
    path: 'mainBoard',
    component: MainBoardComponent,
    children: [
      {
        path: 'organizationOwner',
        loadChildren: () =>
          import('./organization-owner/organization-owner.module').then(
            (m) => m.OrganizationOwnerModule
          ),
        canActivate: [roleAuthGuard],
        data: {
          Roles: [
            'OrganizationOwner',
          ],
        },
      },
      {
        path: 'organizationAdmin',
        loadChildren: () =>
          import('./organization-admin/organization-admin.module').then(
            (m) => m.OrganizationAdminModule
          ),
        canActivate: [roleAuthGuard],
        data: { Roles: ['OrganizationOwner', 'OrganizationAssetManager'] },
      },
      {
        path: 'organizationEmployee',
        loadChildren: () =>
          import('./organization-employee/organization-employee.module').then((m) => m.OrganizationEmployeeModule),
        canActivate: [roleAuthGuard],
        data: { Roles: ['OrganizationOwner', 'OrganizationAssetManager', 'OrganizationEmployee'] },
      },
      {
        path: 'vendor',
        loadChildren: () =>
          import('./vendor/vendor.module').then((m) => m.VendorModule),
        canActivate: [roleAuthGuard],
        data: { Roles: ['Vendor'] },
      }
    ],
  },
  { path: '**', component: ErrorPageComponent, title: 'AssetIn' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BoardRoutingModule { }
