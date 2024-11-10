import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStaffProfileComponent } from './add-staff-profile.component';

describe('AddStaffProfileComponent', () => {
  let component: AddStaffProfileComponent;
  let fixture: ComponentFixture<AddStaffProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddStaffProfileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddStaffProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
