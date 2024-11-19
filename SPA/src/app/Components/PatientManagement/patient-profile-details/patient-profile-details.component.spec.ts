import {ComponentFixture, TestBed} from '@angular/core/testing';
import {PatientProfileDetailsComponent} from './patient-profile-details.component';

describe('PatientDetailsComponent', () => {
  let component: PatientProfileDetailsComponent;
  let fixture: ComponentFixture<PatientProfileDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientProfileDetailsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PatientProfileDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
