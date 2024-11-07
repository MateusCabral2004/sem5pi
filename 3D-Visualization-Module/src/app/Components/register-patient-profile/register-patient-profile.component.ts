import { Router } from '@angular/router';
import { PatientProfile } from '../../Domain/PatientProfile';
import { Component } from '@angular/core';
import {PatientProfileService} from '../../services/PatientProfileService/patient-profile-service';


@Component({
  selector: 'app-register-patient-profile',
  templateUrl: './register-patient-profile.component.html',
  styleUrls: ['./register-patient-profile.component.css']
})

export class RegisterPatientProfileComponent {
  patient: PatientProfile ={
     firstName: '',
      lastName : '',
      dateOfBirth : '',
      phoneNumber :'',
      emailAddress : '',
      medicalHistory:''
};

  constructor(private patientProfileService: PatientProfileService, private router: Router) {}

  registerPatientProfile(){
    this.patientProfileService.registerPatientProfile(this.patient).subscribe(
      (response)=>{
        alert("Patient Profile Registered Successfully");
        this.router.navigate(['/admin/patientProfileManagement']);
        console.log('Success', response);
      },
      (error)=>{
        console.error('Error:', error);
        alert('Error Registering Patient Profile: '+ (error.error || 'An unknown error occured.'));
      }
    );
  }

  resetFields(){
      this.patient={
        firstName: '',
        lastName : '',
        dateOfBirth : '',
        phoneNumber :'',
        emailAddress : '',
        medicalHistory:''
      };
  }

}
