import { TestBed } from '@angular/core/testing';

import { UpdateProductServiceService } from './update-product-service.service';

describe('UpdateProductServiceService', () => {
  let service: UpdateProductServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdateProductServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
