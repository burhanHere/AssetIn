import { TestBed } from '@angular/core/testing';

import { RouteChangeDetectionService } from './route-change-detection.service';

describe('RouteChangeDetectionService', () => {
  let service: RouteChangeDetectionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RouteChangeDetectionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
