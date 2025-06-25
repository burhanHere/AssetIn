import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { JwtService } from '../../../core/services/jwt/jwt.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {
  @ViewChild('imageUpload') imageUpload!: ElementRef<HTMLInputElement>;
  public profilePicture: string;
  public selectedFile: File | null;
  private jwtService: JwtService = inject(JwtService);
  public userInfoForm: FormGroup;
  public isEditMode: boolean;
  public user: any;
  public products: any[];
  public isLoading: boolean;
  public showAlert: boolean;
  public alertTitle: string;
  public alertMessage: string;

  constructor() {
    this.userInfoForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      contactPersonName: new FormControl('', [Validators.required]),
      userEmail: new FormControl('', [Validators.required, Validators.email]),
      userPhone: new FormControl('', [Validators.required, Validators.maxLength(13),
      Validators.minLength(13),
      Validators.pattern(/^\+\d{1,3}\d{9,12}$/)]),
      userAddress: new FormControl('', [Validators.required]),
    });
    this.isEditMode = false;
    this.user = {};
    this.products = [];
    this.isLoading = false;
    this.showAlert = false;
    this.alertTitle = '';
    this.alertMessage = '';
    this.profilePicture = '';
    this.selectedFile = null;
  }

  ngOnInit() {
    // call user detail get api
    this.getUserInfo();
  }

  private getUserInfo(): void {
    // this.isLoading = true;
  }

  private setValidatorData() {
    this.userInfoForm.controls['userName'].setValue(this.user.userName);
    this.userInfoForm.controls['contactPersonName'].setValue(this.user.contactPerson);
    this.userInfoForm.controls['userEmail'].setValue(this.user.email);
    this.userInfoForm.controls['userPhone'].setValue(this.user.phoneNumber);
    this.userInfoForm.controls['userAddress'].setValue(this.user.officeAddress);
  }


  public toggleEdit() {
    this.isEditMode = !this.isEditMode;
  }

  public saveChanges() {
    if (this.userInfoForm.valid) {
      this.isLoading = true;
      const tempJwt = sessionStorage.getItem('auth-jwt') || '';
      let claims;
      if (tempJwt) {
        claims = this.jwtService.getTokenClaims(tempJwt);
      }
      const userData = {
        // add user data in the object
      }
      // call user profile update settings
    } else {
      this.userInfoForm.markAllAsTouched();
    }
  }

  public cancelChanges() {
    // Revert to original data
    this.isEditMode = false;
    this.userInfoForm.reset();
    this.setValidatorData();
  }

  public onDeleteImage(): void {
    this.selectedFile = null;
    if (this.imageUpload) {
      this.imageUpload.nativeElement.value = '';
    }
  }

  public onUploadImage() {
    this.imageUpload.nativeElement.click();
  }

  public handleFileInput(event: Event): void {
    this.onFileSelected(event).subscribe({
      next: (file) => {
        this.selectedFile = file;
        this.uploadToServer(); // Only called after file is read and valid
      },
      error: (err) => {
        this.alertTitle = 'Error';
        this.alertMessage = err;
        this.showAlert = true;
        this.onDeleteImage();
      }
    });
  }

  private uploadToServer(): void {
    if (this.selectedFile) {
      this.isLoading = true;
      const formData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);
      // call user profile picture update api
    } else {
      this.alertTitle = 'Error';
      this.alertMessage = 'No file selected for upload.';
      this.showAlert = true;
    }
  }

  private onFileSelected(event: Event): Observable<File> {
    return new Observable<File>((observer) => {
      const input = event.target as HTMLInputElement;

      if (!input.files || input.files.length === 0) {
        observer.error('No file selected.');
        return;
      }

      const file = input.files[0];

      // Validate type
      if (!file.type.startsWith('image/')) {
        observer.error('Please select a valid image file (jpg, jpeg, png, gif).');
        return;
      }

      // Validate size
      if (file.size > 5 * 1024 * 1024) {
        observer.error('File size must be less than 5MB.');
        return;
      }

      // FileReader for preview
      const reader = new FileReader();
      reader.onload = () => {
        this.profilePicture = reader.result as string;
        observer.next(file);
        observer.complete();
      };
      reader.onerror = () => {
        observer.error('Error reading file.');
      };

      reader.readAsDataURL(file);
    });
  }
}
