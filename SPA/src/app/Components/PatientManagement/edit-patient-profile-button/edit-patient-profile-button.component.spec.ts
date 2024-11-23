import {EditPatientProfileButtonComponent} from './edit-patient-profile-button.component';
import {ComponentFixture, TestBed} from '@angular/core/testing';

describe('EditPatientProfileButtonComponent', () => {
  let component: EditPatientProfileButtonComponent;
  let fixture: ComponentFixture<EditPatientProfileButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditPatientProfileButtonComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(EditPatientProfileButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
