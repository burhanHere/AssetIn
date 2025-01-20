import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationAdminAssetsComponent } from './organization-admin-assets.component';

describe('OrganizationAdminAssetsComponent', () => {
  let component: OrganizationAdminAssetsComponent;
  let fixture: ComponentFixture<OrganizationAdminAssetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationAdminAssetsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationAdminAssetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
