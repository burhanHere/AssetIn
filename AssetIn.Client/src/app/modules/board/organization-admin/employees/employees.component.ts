import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormControl,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent implements OnInit {
  public selectedEmployee: any = null;
  public showErrorMessage: boolean;
  public showForm: boolean;
  public employeeForm: FormGroup;
  public submitted: boolean;
  public errorMessage: string;
  public savedData: any = null;
  public isToggled: boolean = false;

  constructor() {
    this.employeeForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', [Validators.required]),
      gender: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required]),
    });

    this.showErrorMessage = false;
    this.showForm = false;
    this.submitted = false;
    this.errorMessage = '';
  }

  employees = [
    {
      name: 'James Jutt',
      role: 'Organizational Owner',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Male',
      dateOfBirth: '24/1/1995',
      active: true,
      image: 'icons/emp_avatar.png',
    },
    {
      name: 'Julli Gujar',
      role: 'IT Administrator',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Female',
      dateOfBirth: '24/1/1995',
      active: true,
      image: 'icons/emp_avatar.png',
    },
    {
      name: 'George Butt',
      role: 'Asset Manager',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Male',
      dateOfBirth: '24/1/1995',
      active: true,
      image: 'icons/emp_avatar.png',
    },
    {
      name: 'Chaudhary Marco',
      role: 'Employee',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Male',
      dateOfBirth: '24/1/1995',
      active: false,
      image: 'icons/emp_avatar.png',
    },
    {
      name: 'Arthur Bhati',
      role: 'Organizational Owner',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Male',
      dateOfBirth: '24/1/1995',
      active: true,
      image: 'icons/emp_avatar.png',
    },
    {
      name: 'Amelia Malik',
      role: 'Asset Manager',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Female',
      dateOfBirth: '24/1/1995',
      active: true,
      image: 'icons/emp_avatar.png',
    },
    {
      name: 'Mian Thomas',
      role: 'Manager',
      email: 'georgr.butt@123.com',
      phone: '+92-xxx-xxxxxxx',
      gender: 'Male',
      dateOfBirth: '24/1/1995',
      active: false,
      image: 'icons/emp_avatar.png',
    },
  ];


  ngOnInit(): void {
    // Initialization logic can go here
  }

  public selectEmployee(employee: any): void {
    this.selectedEmployee = employee;
    this.isToggled = employee.active;
  }

  public closeModal(): void {
    this.selectedEmployee = null;
  }


  public revokePrivileges(): void {
    if (this.selectedEmployee) {
      this.selectedEmployee.revokePrivileges =
        !this.selectedEmployee.revokePrivileges;
    }
    if (this.selectedEmployee.revokePrivileges) {
      alert(
        `${this.selectedEmployee.userName}'s privileges have been revoked.`
      );
    } else {
      alert(
        `${this.selectedEmployee.userName}'s privileges have been restored.`
      );
    }
  }



 public onToggle(event: Event): void {
  const checkbox = event.target as HTMLInputElement;
  this.isToggled = checkbox.checked;

  if (this.selectedEmployee) {
    this.selectedEmployee.active = this.isToggled;
    console.log(
      `${this.selectedEmployee.name} account status: ${this.isToggled ? 'Active' : 'Inactive'}`
    );
  }
}



  public onAddNewEmployee() {
    debugger;
    this.showForm = true;
  }

  public openCloseNewEmployeeForm(): void {
    this.employeeForm.reset();
    this.showForm = !this.showForm;
  }

  public onSubmit(): void {
    debugger;
    if (this.employeeForm.valid) {
      const newEmployee = this.employeeForm.value;

      console.log('Employee Submitted:', newEmployee);
      this.openCloseNewEmployeeForm();
    }
    else {
         this.showErrorMessage = true;
        alert("Please Fill the form!!!")
    }

  }
}
