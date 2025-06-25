import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendorDashboardComponent } from './vendor-dashboard/vendor-dashboard.component';
import { ErrorPageComponent } from '../../../shared/components/error-page/error-page.component';
import { AddNewProductComponent } from './add-new-product/add-new-product.component';

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
    path: 'addNewProduct',
    component: AddNewProductComponent,
    title: 'Add New Product'
  },
  { path: '**', component: ErrorPageComponent, title: 'Dashboard' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorRoutingModule { }
