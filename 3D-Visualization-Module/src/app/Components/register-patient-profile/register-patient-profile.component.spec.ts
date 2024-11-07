import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterPatientProfileComponent } from './register-patient-profile.component';
import {AddOperationTypeComponent} from '../add-operation-type/add-operation-type.component';

describe('RegisterPatientProfileComponent', () => {
  let component: RegisterPatientProfileComponent;
  let fixture: ComponentFixture<RegisterPatientProfileComponent>;

  beforeEach(async ()=>{
    await TestBed.configureTestingModule({
        imports:[RegisterPatientProfileComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AddOperationTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  })


  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
