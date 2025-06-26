import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OrganizationManagementService } from '../../../../core/services/organizationManagement/organization-management.service';
import { Organization } from '../../../../core/models/organization';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { domainSuffixValidator } from '../../../../shared/validators/domain-siffix.validator';

@Component({
  selector: 'app-organizations-dashboard',
  templateUrl: './organizations-dashboard.component.html',
  styleUrl: './organizations-dashboard.component.css',
})
export class OrganizationsDashboardComponent implements OnInit {
  private organizationManagementService: OrganizationManagementService = inject(
    OrganizationManagementService
  );
  private router: Router = inject(Router);
  public createOrganizationForm: FormGroup;
  public showNewOrganizationCreationForm: boolean;
  public organizations: Array<any>;
  public isLoading: boolean;
  public showAlert: boolean;
  public alertTitle: string;
  public alertMessage: string;
  public showFormError: boolean;

  constructor() {
    this.createOrganizationForm = new FormGroup({
      organizationName: new FormControl('', [Validators.required]),
      organizationDescription: new FormControl('', [Validators.required]),
      organizationDomain: new FormControl('', [
        Validators.required,
        domainSuffixValidator(),
      ]),
    });
    this.showNewOrganizationCreationForm = false;
    this.organizations = [];
    this.isLoading = false;
    this.showAlert = false;
    this.alertTitle = '';
    this.alertMessage = '';
    this.showFormError = false;
    if (sessionStorage.getItem('targetOrganizationID')) {
      sessionStorage.removeItem('targetOrganizationID');
    }
  }

  ngOnInit(): void {
    this.getAllOrganization();
  }

  private getAllOrganization(): void {
    this.isLoading = true;
    this.organizationManagementService
      .GetOrganizationsListForOrganizationsDashboard()
      .subscribe(
        (responce: any) => {
          this.isLoading = false;
          this.organizations = responce.responseData;
        },
        (error: HttpErrorResponse) => {
          this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
          this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
          this.isLoading = false;
          this.showAlert = true;
        }
      );
  }

  public openCloseNewOrganizationForm(): void {
    this.createOrganizationForm.reset();
    this.showNewOrganizationCreationForm =
      !this.showNewOrganizationCreationForm;
  }

  public createOrganization(): void {
    this.isLoading = true;
    if (this.createOrganizationForm.valid) {
      this.showNewOrganizationCreationForm = false;
      const newOrganizationData: Organization = {
        organzationId: 0,
        organizationName:
          this.createOrganizationForm.controls['organizationName'].value,
        description:
          this.createOrganizationForm.controls['organizationDescription'].value,
        organizationDomain:
          this.createOrganizationForm.controls['organizationDomain'].value,
      };
      this.createOrganizationForm.reset();
      this.organizationManagementService
        .CreateOrganization(newOrganizationData)
        .subscribe(
          (responce: any) => {
            this.isLoading = false;
          },
          (error: HttpErrorResponse) => {
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
            this.isLoading = false;
            this.showAlert = true;
          },
          () => {
            this.getAllOrganization();
          }
        );
    } else {
      this.showFormError = true;
      this.isLoading = false;
    }
  }

  public openTargetOrganizationDashboard(targetOrganization: any): void {
    this.router.navigateByUrl('/board/mainBoard/organizationAdmin');
    sessionStorage.setItem(
      'targetOrganizationID',
      targetOrganization.organizationID
    );
  }
}
