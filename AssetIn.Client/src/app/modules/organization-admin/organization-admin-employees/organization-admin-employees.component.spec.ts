import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationAdminEmployeesComponent } from './organization-admin-employees.component';

describe('OrganizationAdminEmployeesComponent', () => {
  let component: OrganizationAdminEmployeesComponent;
  let fixture: ComponentFixture<OrganizationAdminEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationAdminEmployeesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationAdminEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
