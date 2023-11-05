import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUserByAdminComponent } from './create-user-by-admin.component';

describe('CreateUserByAdminComponent', () => {
  let component: CreateUserByAdminComponent;
  let fixture: ComponentFixture<CreateUserByAdminComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateUserByAdminComponent]
    });
    fixture = TestBed.createComponent(CreateUserByAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
