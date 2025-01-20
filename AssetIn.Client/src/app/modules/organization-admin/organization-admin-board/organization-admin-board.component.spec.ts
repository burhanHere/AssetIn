import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationAdminBoardComponent } from './organization-admin-board.component';

describe('OrganizationAdminBoardComponent', () => {
  let component: OrganizationAdminBoardComponent;
  let fixture: ComponentFixture<OrganizationAdminBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationAdminBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationAdminBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
