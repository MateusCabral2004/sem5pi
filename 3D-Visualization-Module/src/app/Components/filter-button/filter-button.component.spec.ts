import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterStaffButtonComponent } from './filter-staff-button.component';

describe('FilterStaffButtonComponent', () => {
  let component: FilterStaffButtonComponent;
  let fixture: ComponentFixture<FilterStaffButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilterStaffButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FilterStaffButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
