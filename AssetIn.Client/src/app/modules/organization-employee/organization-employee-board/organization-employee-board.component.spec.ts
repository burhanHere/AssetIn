import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationEmployeeBoardComponent } from './organization-employee-board.component';

describe('OrganizationEmployeeBoardComponent', () => {
  let component: OrganizationEmployeeBoardComponent;
  let fixture: ComponentFixture<OrganizationEmployeeBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationEmployeeBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationEmployeeBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
