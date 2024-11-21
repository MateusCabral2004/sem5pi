import {DeletePatientProfileButtonComponent} from './delete-patient-profile-button.component';
import {ComponentFixture, TestBed} from '@angular/core/testing';
describe('DeletePatientProfileButtonComponent', () => {
  let component: DeletePatientProfileButtonComponent;
  let fixture: ComponentFixture<DeletePatientProfileButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeletePatientProfileButtonComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(DeletePatientProfileButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
