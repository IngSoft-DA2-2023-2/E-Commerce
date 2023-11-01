import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavBarViewComponent } from './nav-bar-view.component';

describe('NavBarViewComponent', () => {
  let component: NavBarViewComponent;
  let fixture: ComponentFixture<NavBarViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavBarViewComponent]
    });
    fixture = TestBed.createComponent(NavBarViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
