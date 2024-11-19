import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterPatientProfileComponent } from './register-patient-profile.component';

describe('RegisterPatientProfileComponent', () => {
  let component: RegisterPatientProfileComponent;
  let fixture: ComponentFixture<RegisterPatientProfileComponent>;

  beforeEach(async ()=>{
    await TestBed.configureTestingModule({
        imports:[RegisterPatientProfileComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RegisterPatientProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  })


  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
