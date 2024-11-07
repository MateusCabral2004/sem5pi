import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStaffProfileButtonComponent } from './edit-staff-profile-button.component';

describe('EditStaffProfileButtonComponent', () => {
  let component: EditStaffProfileButtonComponent;
  let fixture: ComponentFixture<EditStaffProfileButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditStaffProfileButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditStaffProfileButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
