import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationsDashboardComponent } from './organizations-dashboard.component';

describe('OrganizationsDashboardComponent', () => {
  let component: OrganizationsDashboardComponent;
  let fixture: ComponentFixture<OrganizationsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationsDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
