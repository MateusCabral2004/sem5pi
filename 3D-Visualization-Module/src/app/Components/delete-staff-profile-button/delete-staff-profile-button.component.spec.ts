import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteStaffProfileButtonComponent } from './delete-staff-profile-button.component';

describe('DeleteStaffProfileButtonComponent', () => {
  let component: DeleteStaffProfileButtonComponent;
  let fixture: ComponentFixture<DeleteStaffProfileButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteStaffProfileButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteStaffProfileButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
