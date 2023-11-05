import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateUserByAdminViewComponent } from './update-user-by-admin-view.component';

describe('UpdateUserByAdminViewComponent', () => {
  let component: UpdateUserByAdminViewComponent;
  let fixture: ComponentFixture<UpdateUserByAdminViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateUserByAdminViewComponent]
    });
    fixture = TestBed.createComponent(UpdateUserByAdminViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
