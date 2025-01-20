import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationAdminAssetRequestsComponent } from './organization-admin-asset-requests.component';

describe('OrganizationAdminAssetRequestsComponent', () => {
  let component: OrganizationAdminAssetRequestsComponent;
  let fixture: ComponentFixture<OrganizationAdminAssetRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationAdminAssetRequestsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationAdminAssetRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
