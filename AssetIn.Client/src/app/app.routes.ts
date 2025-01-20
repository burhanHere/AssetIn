import { Routes } from '@angular/router';
import { ErrorPageComponent } from './shared/components/error-page/error-page.component';
import { roleAuthGuard } from './core/guards/roleAuthGuard/role-auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./modules/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'organizationOwner',
    loadChildren: () =>
      import('./modules/organization-owner/organization-owner.module').then(
        (m) => m.OrganizationOwnerModule
      ),
    canActivate: [roleAuthGuard],
    data: {
      roles: [
        'OrganizationOwner',
      ],
    },
  },
  {
    path: 'organizationAdmin',
    loadChildren: () =>
      import('./modules/organization-admin/organization-admin.module').then(
        (m) => m.OrganizationAdminModule
      ),
    canActivate:[roleAuthGuard],
    data: { roles:['OrganizationOwner','OrganizationAssetManager',] },
  },
  {
    path: 'organizationEmployee',
    loadChildren: () =>
      import('./modules/organization-employee/organization-employee.module').then((m) => m.OrganizationEmployeeModule),
    canActivate: [roleAuthGuard],
    data: { roles: ['OrganizationOwner','OrganizationAssetManager','OrganizationEmployee',] },
  },
  {
    path: 'vendor',
    loadChildren: () =>
      import('./modules/vendor/vendor.module').then((m) => m.VendorModule),
    canActivate: [roleAuthGuard],
    data: { roles: ['Vendor',] },
  },
  {
    path: '**',
    component: ErrorPageComponent,
    title: 'Error',
  },
];
