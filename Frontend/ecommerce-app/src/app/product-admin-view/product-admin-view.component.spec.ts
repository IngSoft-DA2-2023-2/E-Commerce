import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductAdminViewComponent } from './product-admin-view.component';

describe('ProductAdminViewComponent', () => {
  let component: ProductAdminViewComponent;
  let fixture: ComponentFixture<ProductAdminViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductAdminViewComponent]
    });
    fixture = TestBed.createComponent(ProductAdminViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
