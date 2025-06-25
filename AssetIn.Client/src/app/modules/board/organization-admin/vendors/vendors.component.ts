import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrl: './vendors.component.css',
})
export class VendorsComponent implements OnInit {
  public vendors: any[];
  public products: any[];
  public selectedVendor: any = null;
  public showVendor: boolean ;

  constructor() {
    this.vendors = [
      {
        name: 'Jaidi Pan Shop',
        email: 'jaidipan.shop@gmail.com',
        address: 'Shop 29, 1 Floor, Hafeez Center, Lahore.',
        phoneNumber: '0300XXXXXXX',
        logo: 'https://via.placeholder.com/100x100.png?text=J',
      },
      {
        name: 'Tech Planet',
        email: 'contact@techplanet.com',
        address: 'Suite 14, Mall Plaza, Rawalpindi.',
        phoneNumber: '0311XXXXXXX',
        logo: 'https://via.placeholder.com/100x100.png?text=T',
      },
      {
        name: 'Digital Shoppe',
        email: 'hello@digitalshoppe.pk',
        address: 'Shop 5, Ground Floor, Giga Mall, Islamabad.',
        phoneNumber: '0322XXXXXXX',
        logo: 'https://via.placeholder.com/100x100.png?text=D',
      },
      {
        name: 'Tech Planet',
        email: 'contact@techplanet.com',
        address: 'Suite 14, Mall Plaza, Rawalpindi.',
        phoneNumber: '0311XXXXXXX',
        logo: 'https://via.placeholder.com/100x100.png?text=T',
      },
      {
        name: 'Tech Planet',
        email: 'contact@techplanet.com',
        address: 'Suite 14, Mall Plaza, Rawalpindi.',
        phoneNumber: '0311XXXXXXX',
        logo: 'https://via.placeholder.com/100x100.png?text=T',
      }
    ];
    this.products = [
      {
        name: 'Logitech K380s',
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia…',
      },
      {
        name: 'Logitech K380s',
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia…',
      },
      {
        name: 'Logitech K380s',
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. ',
      },
      {
        name: 'Logitech K380s',
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia…',
      },
      {
        name: 'Logitech K380s',
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia…',
      },
    ];
    this.selectedVendor = [];
    this.showVendor = false;
  }
  ngOnInit(): void {}

  public showVendorDetails(vendor: any): void {
    // get vendor info
    // get product info
    this.showVendor = true;
    this.selectedVendor = vendor;

  }
}
