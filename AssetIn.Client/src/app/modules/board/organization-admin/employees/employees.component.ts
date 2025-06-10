import {
  Component,
  ElementRef,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  FormGroup,
  Validators,
  FormControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { EmployeeManagementService } from '../../../../core/services/EmployeeManagement/employee-management.service';
import { lockUnlockUser } from '../../../../core/models/lockUnlockUser';
import { NewEmployee } from '../../../../core/models/newEmployee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent implements OnInit {
  private employeeManagementService: EmployeeManagementService = inject(
    EmployeeManagementService
  );
  private organizationId: number;
  public selectedEmployee: any = null;
  public showErrorMessage: boolean;
  public showForm: boolean;
  public employeeForm: FormGroup;
  public submitted: boolean;
  public errorMessage: string;
  public savedData: any = null;
  public isToggled: boolean = false;
  public employees: any[];
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public organizationDomain: string;



  constructor() {
    // this.organizationDomain =
    //   sessionStorage.getItem('targetOrganizationDomain') || '';
    this.organizationDomain = '@domain.com';
    this.employeeForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      email: new FormControl(this.organizationDomain),
      phone: new FormControl('', [Validators.required]),
      gender: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required]),
    });


    this.showErrorMessage = false;
    this.showForm = false;
    this.submitted = false;
    this.errorMessage = '';
    this.employees = [];
    this.organizationId =
      Number(sessionStorage.getItem('targetOrganizationID')) || 0;
    this.isLoading = false;
    this.showAlert = false;
    this.alertMessage = '';
    this.alertTitle = '';
  }

  ngOnInit(): void {
    this.getallEmployees();
  }

  public getallEmployees(): void {
    this.isLoading = true;
    this.showAlert = false;
    this.employeeManagementService
      .GetEmployeeList(this.organizationId)
      .subscribe(
        (response) => {
          this.employees = response.responseData;
          this.isLoading = false;
        },
        (error) => {
          this.alertMessage = error.error.responseData[1];
          this.alertTitle = error.error.responseData[0];
          this.showAlert = true;
          this.isLoading = false;
        }
      );
  }

  public selectEmployee(employee: any): void {
    this.selectedEmployee = employee;
    this.isToggled = employee.status;
  }

  public closeModal(): void {
    this.selectedEmployee = null;
  }

  public revokeGrantPrivileges(task: string): void {
    this.isLoading = true;
    const userData: lockUnlockUser = {
      targetOrganizationId: this.organizationId,
      targetUserId: this.selectedEmployee.id,
    };

    if (task === 'Grant') {
      this.employeeManagementService
        .GrantAssetManagerPreviliges(userData)
        .subscribe(
          (response) => {
            this.selectedEmployee.roleName = 'OrganizationAssetManager'
            this.alertTitle = response.responseData[0];
            this.alertMessage = response.responseData[1];
            this.isLoading = false;
          },
          (error) => {
            this.alertTitle = error.error.responseData[0];
            this.alertMessage = error.error.responseData[1];
            this.showAlert = true;
            this.isLoading = false;
          },
          () => {
            this.getallEmployees();
            this.showAlert = true;
          }
        );
    } else if (task === 'Revoke') {
      this.employeeManagementService
        .RevokeAssetManagerPreviliges(userData)
        .subscribe(
          (response) => {
            this.selectedEmployee.roleName = 'OrganizationEmployee'
            this.alertTitle = response.responseData[0];
            this.alertMessage = response.responseData[1];
            this.isLoading = false;
          },
          (error) => {
            this.alertTitle = error.error.responseData[0];
            this.alertMessage = error.error.responseData[1];
            this.showAlert = true;
            this.isLoading = false;
          },
          () => {
            this.getallEmployees();
            this.showAlert = true;
          }
        );
    }
  }

  public onToggle(event: Event): void {
    this.isLoading = true;
    const userData: lockUnlockUser = {
      targetOrganizationId: this.organizationId,
      targetUserId: this.selectedEmployee.id,
    };

    const checkbox = event.target as HTMLInputElement;
    this.isToggled = checkbox.checked;

    if (this.selectedEmployee) {
      this.selectedEmployee.active = this.isToggled;
      if (this.isToggled) {
        this.employeeManagementService.UnlockUserAcount(userData).subscribe(
          (response) => {
            this.alertTitle = response.responseData[0];
            this.alertMessage = response.responseData[1];
            this.isLoading = false;
          },
          (error) => {
            this.alertTitle = error.error.responseData[0];
            this.alertMessage = error.error.responseData[1];
            this.showAlert = true;
            this.isLoading = false;
          },
          () => {
            this.getallEmployees();
            this.showAlert = true;
          }
        );
      } else {
        this.employeeManagementService.LockUserAcount(userData).subscribe(
          (response) => {
            this.alertTitle = response.responseData[0];
            this.alertMessage = response.responseData[1];
            this.isLoading = false;
          },
          (error) => {

            this.alertTitle = error.error.responseData[0];
            this.alertMessage = error.error.responseData[1];
            this.showAlert = true;
            this.isLoading = false;
          },
          () => {
            this.getallEmployees();
            this.showAlert = true;
          }
        );
      }
    }
  }

  public onAddNewEmployee() {
    this.showForm = true;
  }

  public openCloseNewEmployeeForm(): void {
    this.employeeForm.reset();
    this.showForm = !this.showForm;
  }

  public onSubmit(): void {
    this.isLoading = true;
    if (this.employeeForm.valid) {
      this.showForm = false;
      const newUserData: NewEmployee = {
        organizationId: this.organizationId,
        userName: this.employeeForm.controls['userName'].value,
        phoneNumber: this.employeeForm.controls['phone'].value,
        gender: this.employeeForm.controls['gender'].value,
        dateOfBirth: this.employeeForm.controls['dateOfBirth'].value,
      };
      this.employeeForm.reset();

      this.employeeManagementService.CreateEmployee(newUserData).subscribe(
        (response) => {
          this.isLoading = false;
        },
        (error) => {
          this.alertTitle = error.error.responseData[0];
          this.alertMessage = error.error.responseData[1];
          this.showAlert = true;
          this.isLoading = false;
        },
        () => {
          this.getallEmployees();
        }
      );
    } else {
      this.showErrorMessage = true;
      alert('Please Fill the form!!!');
      this.isLoading = false;
      this.employeeForm.markAllAsTouched();
    }
  }
}
