import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateProductViewComponent } from './update-product-view.component';

describe('UpdateProductViewComponent', () => {
  let component: UpdateProductViewComponent;
  let fixture: ComponentFixture<UpdateProductViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateProductViewComponent]
    });
    fixture = TestBed.createComponent(UpdateProductViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
