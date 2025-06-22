import { TestBed } from '@angular/core/testing';

import { CrystalReportingService } from './crystal-reporting.service';

describe('CrystalReportingService', () => {
  let service: CrystalReportingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CrystalReportingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
