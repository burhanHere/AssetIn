import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateAssetComponent } from './add-update-asset.component';

describe('AddUpdateAssetComponent', () => {
  let component: AddUpdateAssetComponent;
  let fixture: ComponentFixture<AddUpdateAssetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddUpdateAssetComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUpdateAssetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
