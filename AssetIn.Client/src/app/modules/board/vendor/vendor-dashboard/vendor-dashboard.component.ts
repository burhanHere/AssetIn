import { Component } from '@angular/core';

@Component({
  selector: 'app-vendor-dashboard',
  templateUrl: './vendor-dashboard.component.html',
  styleUrl: './vendor-dashboard.component.css'
})
export class VendorDashboardComponent {

  vendor = {
    fullName: 'Jaidi Pan Shop',
    email: 'jaidipan.shop@gmail.com',
    address: 'Shop 29, 1 Floor , Hafeez Center, Lahore.',
    countryCode: '+92',
    phoneNumber: '0300XXXXXXX',
    logoUrl: 'assets/logo.png' // Adjust if necessary
  };

  products = [
    {
      id: 1,
      name: 'Logitech K380s',
      description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia...'
    },
    {
      id: 2,
      name: 'Logitech K380s',
      description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia...'
    },
    {
      id: 3,
      name: 'Logitech K380s',
      description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia...'
    }
  ];

  onEdit() {
    console.log('Edit vendor info clicked');
  }

  onAddProduct() {
    console.log('Add new product clicked');
  }

}