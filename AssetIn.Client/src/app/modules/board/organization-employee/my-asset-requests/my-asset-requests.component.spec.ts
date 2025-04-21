import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAssetRequestsComponent } from './my-asset-requests.component';

describe('MyAssetRequestsComponent', () => {
  let component: MyAssetRequestsComponent;
  let fixture: ComponentFixture<MyAssetRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyAssetRequestsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyAssetRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
