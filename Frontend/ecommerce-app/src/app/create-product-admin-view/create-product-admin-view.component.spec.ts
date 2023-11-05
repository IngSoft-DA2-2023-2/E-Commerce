import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateProductAdminViewComponent } from './create-product-admin-view.component';

describe('CreateProductAdminViewComponent', () => {
  let component: CreateProductAdminViewComponent;
  let fixture: ComponentFixture<CreateProductAdminViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateProductAdminViewComponent]
    });
    fixture = TestBed.createComponent(CreateProductAdminViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
