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
  { path: 'board', loadChildren: () => import('./modules/board/board.module').then((m) => m.BoardModule), canActivate: [roleAuthGuard], data: { Roles: ['OrganizationOwner', 'OrganizationAssetManager', 'OrganizationEmployee', 'Vendor'] } },
  {
    path: '**',
    component: ErrorPageComponent,
    title: 'Error',
  },
];
