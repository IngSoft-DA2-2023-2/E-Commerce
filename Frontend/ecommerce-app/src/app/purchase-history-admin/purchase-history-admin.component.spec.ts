import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseHistoryAdminComponent } from './purchase-history-admin.component';

describe('PurchaseHistoryAdminComponent', () => {
  let component: PurchaseHistoryAdminComponent;
  let fixture: ComponentFixture<PurchaseHistoryAdminComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PurchaseHistoryAdminComponent]
    });
    fixture = TestBed.createComponent(PurchaseHistoryAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
