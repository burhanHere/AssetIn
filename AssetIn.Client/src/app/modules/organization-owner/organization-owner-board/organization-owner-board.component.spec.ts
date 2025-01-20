import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationOwnerBoardComponent } from './organization-owner-board.component';

describe('OrganizationOwnerBoardComponent', () => {
  let component: OrganizationOwnerBoardComponent;
  let fixture: ComponentFixture<OrganizationOwnerBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationOwnerBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationOwnerBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
