import { TestBed } from '@angular/core/testing';

import { AssetRequestManagementService } from './asset-request-management.service';

describe('AssetRequestManagementService', () => {
  let service: AssetRequestManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssetRequestManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
