import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorBoardComponent } from './vendor-board.component';

describe('VendorBoardComponent', () => {
  let component: VendorBoardComponent;
  let fixture: ComponentFixture<VendorBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VendorBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VendorBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
