import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignupViewComponent } from './signup-view.component';

describe('SignupViewComponent', () => {
  let component: SignupViewComponent;
  let fixture: ComponentFixture<SignupViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SignupViewComponent]
    });
    fixture = TestBed.createComponent(SignupViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
