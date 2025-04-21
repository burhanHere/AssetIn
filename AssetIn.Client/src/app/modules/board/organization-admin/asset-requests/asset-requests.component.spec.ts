import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssetRequestsComponent } from './asset-requests.component';

describe('AssetRequestsComponent', () => {
  let component: AssetRequestsComponent;
  let fixture: ComponentFixture<AssetRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssetRequestsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssetRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
