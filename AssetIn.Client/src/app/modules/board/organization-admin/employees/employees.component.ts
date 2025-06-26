import {
  Component,
  ElementRef,
  inject,
  OnInit,
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
import { ageGreaterThan18Validator } from '../../../../shared/validators/age-gratter-then';
import { UpdateEmployee } from '../../../../core/models/UpdateEmployee';

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
  public selectedEmployee: any;
  public showForm: boolean;
  public employeeForm: FormGroup;
  public submitted: boolean;
  public errorMessage: string;
  public savedData: any;
  public isToggled: boolean;
  public employees: any[];
  public isLoading: boolean;
  public showAlert: boolean;
  public alertMessage: string;
  public alertTitle: string;
  public organizationDomain: string;
  private showFormForEdit: boolean;
  private updateEmployeeId: string;


  constructor() {
    this.organizationDomain =
      sessionStorage.getItem('targetOrganizationDomain') || '';
    this.employeeForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z0-9]*$')]),
      email: new FormControl(this.organizationDomain),
      phone: new FormControl('', [Validators.required]),
      gender: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required, ageGreaterThan18Validator(18)]),
      // role: new FormControl('', [Validators.required]),
    });


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
    this.showFormForEdit = false;
    this.updateEmployeeId = '';
    this.isToggled = false;
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
          this.employees.sort((a, b) => b.email.localeCompare(a.email));
          this.isLoading = false;
        },
        (error) => {
          this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
          this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
            this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
            this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
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
    this.showFormForEdit = false;
    this.employeeForm.reset();
  }

  public openCloseNewEmployeeForm(): void {
    this.employeeForm.reset();
    this.showForm = !this.showForm;
  }

  public onSubmit(): void {
    this.isLoading = true;
    if (this.employeeForm.valid) {
      if (!this.showFormForEdit) {
        this.createEmployee();
      } else {
        this.showFormForEdit = false;
        this.updateEmployee();
      }
    } else {
      this.isLoading = false;
      this.employeeForm.markAllAsTouched();
    }
  }

  private createEmployee(): void {
    const newUserData: NewEmployee = {
      organizationId: this.organizationId,
      userName: this.employeeForm.controls['userName'].value,
      phoneNumber: this.employeeForm.controls['phone'].value,
      gender: this.employeeForm.controls['gender'].value,
      dateOfBirth: this.employeeForm.controls['dateOfBirth'].value,
    };

    this.employeeManagementService.CreateEmployee(newUserData).subscribe(
      (response) => {
        this.alertTitle = response.responseData?.[0];
        this.alertMessage = response.responseData?.[1];
        this.showAlert = true;
        this.employeeForm.reset();
        this.showForm = false;
        this.isLoading = false;
      },
      (error) => {
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.showAlert = true;
        this.isLoading = false;
      },
      () => {
        this.getallEmployees();
      }
    );
  }

  private updateEmployee(): void {
    const updatedUserData: UpdateEmployee = {
      id: this.updateEmployeeId,
      organizationId: this.organizationId,
      userName: this.employeeForm.controls['userName'].value,
      phoneNumber: this.employeeForm.controls['phone'].value,
      gender: this.employeeForm.controls['gender'].value,
      dateOfBirth: this.employeeForm.controls['dateOfBirth'].value,
    };

    this.employeeManagementService.UpdateEmployee(updatedUserData).subscribe(
      (response) => {
        this.alertTitle = response.responseData[0];
        this.alertMessage = response.responseData[1];
        this.showAlert = true;
        this.employeeForm.reset();
        this.showForm = false;
        this.isLoading = false;
      },
      (error) => {
        this.alertTitle = error.error?.responseData?.[0] || error.error?.message || 'Error';
        this.alertMessage = error.error?.responseData?.[1] || error.error?.message || 'Unknown error occurred';
        this.showAlert = true;
        this.isLoading = false;
      },
      () => {
        this.getallEmployees();
      }
    );
  }

  public toggleEdit(): void {
    this.employeeForm.controls['userName'].setValue(this.selectedEmployee.userName);
    this.employeeForm.controls['email'].setValue(this.selectedEmployee.email);
    this.employeeForm.controls['phone'].setValue(this.selectedEmployee.phoneNumber);
    this.employeeForm.controls['gender'].setValue(this.selectedEmployee.gender);
    this.employeeForm.controls['dateOfBirth'].setValue(this.selectedEmployee.dateOfBirth);
    this.updateEmployeeId = this.selectedEmployee.id;
    this.selectedEmployee = null;
    this.showForm = true;
    this.showFormForEdit = true;
  }
}
