import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SigninViewComponent } from './signin-view.component';

describe('SigninViewComponent', () => {
  let component: SigninViewComponent;
  let fixture: ComponentFixture<SigninViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SigninViewComponent]
    });
    fixture = TestBed.createComponent(SigninViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});