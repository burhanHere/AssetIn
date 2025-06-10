import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BarController } from 'chart.js';

@Component({
  selector: 'app-add-update-asset',
  templateUrl: './add-update-asset.component.html',
  styleUrl: './add-update-asset.component.css'
})
export class AddUpdateAssetComponent {
  
  public assetForm: FormGroup;
  public showErrorMessage: boolean;
  public showForm: boolean;
  public submitted: boolean;
  public errorMessage: string;

  constructor() {
      this.assetForm = new FormGroup({
        assetName: new FormControl('', [Validators.required]),
        assetCategory: new FormControl('', [Validators.required]),
        serialNumber: new FormControl('', [Validators.required]),
        purchasePrice: new FormControl('', [Validators.required]),
        model: new FormControl('', [Validators.required]),
        manufacturer: new FormControl('', [Validators.required]),
        depreciationRate: new FormControl('', [Validators.required]),
        assetIdentificationNumber: new FormControl('', [Validators.required]),
        assetType: new FormControl('', [Validators.required]),
        purchaseDate: new FormControl('', [Validators.required]),
        location: new FormControl('', [Validators.required]),
        description: new FormControl('', [Validators.required]),
        problem: new FormControl('', [Validators.required]),
      });

      this.showErrorMessage = false;
      this.showForm = false;
      this.submitted = false;
      this.errorMessage = '';
    }

  assetCategories: string[] = ['Computer Accessory', 'Stationery', 'Furniture'];
  assetTypes: string[] = ['Laptop', 'Monitor', 'Keyboard', 'Mouse', 'CPU', 'USB', 'Charger'];
  
  previewUrl: string | null = null;
  @ViewChild('fileUploader') fileUploader!: ElementRef<HTMLInputElement>;
  @ViewChild('cameraCapture') cameraCapture!: ElementRef<HTMLInputElement>;

  onSubmit(a: any) {
    console.log('Form submitted');
  }

  onCancel() {
    console.log('Form cancelled');
    this.assetForm.reset(
      {
        assetCategory: '',
        assetType: ''
      }
    );
    this.previewUrl = null;
  }

  onDeleteImage() {
    console.log('Image deleted');
    this.previewUrl = null;
    this.fileUploader.nativeElement.value = '';
    this.cameraCapture.nativeElement.value = '';
  }

  onUploadImage() {
    console.log('Image uploaded');
    this.fileUploader.nativeElement.click();
  }

  onCaptureImage() {
    console.log('Asset captured');
    this.cameraCapture.nativeElement.click();
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
