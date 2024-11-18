import {ComponentFixture, TestBed} from '@angular/core/testing';
import {PatientProfileListComponent} from './patient-profile-list.component';

describe('PatientProfileListComponent', () => {
  let component: PatientProfileListComponent;
  let fixture: ComponentFixture<PatientProfileListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientProfileListComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PatientProfileListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
