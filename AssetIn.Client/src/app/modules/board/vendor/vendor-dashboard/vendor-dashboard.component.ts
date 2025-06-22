import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-vendor-dashboard',
  templateUrl: './vendor-dashboard.component.html',
  styleUrl: './vendor-dashboard.component.css'
})
export class VendorDashboardComponent {

  public isEditMode: boolean = false;

  vendor = {
    fullName: 'Jaidi Pan Shop',
    email: 'jaidipan.shop@gmail.com',
    address: 'Shop 29, 1 Floor , Hafeez Center, Lahore.',
    countryCode: '+92',
    phoneNumber: '0300XXXXXXX'
  };

  tempVendor = {
    fullName: '',
    email: '',
    address: '',
    countryCode: '',
    phoneNumber: ''
  };
  
  ngOnInit() {
    this.tempVendor = { ...this.vendor };
  }

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
    },
    {
      id: 4,
      name: 'Logitech K380s',
      description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia...'
    },
    {
      id: 5,
      name: 'Logitech K380s',
      description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia...'
    }
  ];

  previewUrl: string | null = null;
    @ViewChild('fileUploader') fileUploader!: ElementRef<HTMLInputElement>;
    @ViewChild('cameraCapture') cameraCapture!: ElementRef<HTMLInputElement>;

  toggleEdit() {
    this.tempVendor = { ...this.vendor }; // Make fresh editable copy
    this.isEditMode = !this.isEditMode;
    console.log('Edit mode toggled:', this.isEditMode);
  }

  saveChanges() {
    this.vendor = { ...this.tempVendor }; // Apply changes
    this.isEditMode = false;
    console.log("Vendor changes saved:", this.vendor);
  }

  cancelChanges() {
    this.tempVendor = { ...this.vendor }; // Revert edits
    this.isEditMode = false;
    // Optionally reload original vendor data if needed
    console.log("Vendor changes cancelled");
  }

  onDelete() {
    this.previewUrl = null;
    this.fileUploader.nativeElement.value = '';
    this.cameraCapture.nativeElement.value = '';
    console.log('Image Deleted');
  }

  onUpload() {
    this.fileUploader.nativeElement.click();
    console.log('Image uploaded');
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const file = input.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      // reader.result is a base64 data URL
      this.previewUrl = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

}