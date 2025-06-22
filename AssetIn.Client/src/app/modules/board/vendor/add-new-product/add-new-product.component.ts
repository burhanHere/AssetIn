import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-new-product',
  templateUrl: './add-new-product.component.html',
  styleUrl: './add-new-product.component.css'
})
export class AddNewProductComponent {
  
  public productForm: FormGroup;
  public showErrorMessage: boolean;
  public showForm: boolean;
  public submitted: boolean;
  public errorMessage: string;

  constructor() {
      this.productForm = new FormGroup({
        productName: new FormControl('', [Validators.required]),
        unitPrice: new FormControl('', [Validators.required]),
        model: new FormControl('', [Validators.required]),
        description: new FormControl('', [Validators.required]),
      });

      this.showErrorMessage = false;
      this.showForm = false;
      this.submitted = false;
      this.errorMessage = '';
    }
  
  previewUrl: string | null = null;
  @ViewChild('fileUploader') fileUploader!: ElementRef<HTMLInputElement>;
  @ViewChild('cameraCapture') cameraCapture!: ElementRef<HTMLInputElement>;

  onSubmit(a: any) {
    console.log('Form submitted');
  }

  onCancel() {
    console.log('Form cancelled');
    this.productForm.reset();
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
