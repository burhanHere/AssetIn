import { Routes } from '@angular/router';
import { ErrorPageComponent } from './shared/components/error-page/error-page.component';

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
    path: 'organization',
    loadChildren: () =>
      import('./modules/organization/organization.module').then((m) => m.OrganizationModule),
  },
  {
    path: 'vendor',
    loadChildren: () =>
      import('./modules/vendor/vendor.module').then((m) => m.VendorModule),
  },
  {
    path: '**',
    component: ErrorPageComponent,
    title: 'Error',
  },
];
