import {ActivatedRoute, Router} from '@angular/router';
import {Component, OnInit} from '@angular/core';
import {PatientProfile} from '../../../Domain/PatientProfile';
import {PatientProfileService} from '../../../services/PatientProfileService/patient-profile-service';


@Component({
  selector: 'app-register-patient-profile',
  templateUrl: './register-patient-profile.component.html',
  styleUrls: ['./register-patient-profile.component.css']
})

export class RegisterPatientProfileComponent {
  patientProfile: PatientProfile = {
    firstName: '',
    lastName: '',
    email: '',
    birthDate: '',
    phoneNumber: 0,
    gender:'',
    emergencyContact:'',
  };


  constructor(private router: Router, private patientProfileService: PatientProfileService) {
  }

  resetFields() {
    this.patientProfile = {
      firstName: '',
      lastName: '',
      email: '',
      birthDate: '',
      phoneNumber: 0,
      gender:'',
      emergencyContact:'',
    };
  }
  isPatientProfileValid(): boolean {
    return (
      !!this.patientProfile.firstName &&
      this.patientProfile.lastName !== '' &&
      this.patientProfile.birthDate !== '' &&
      this.patientProfile.email !== '' &&
      this.patientProfile.phoneNumber != 0
    );
  }


  registerPatientProfile()
  {
    this.patientProfileService.registerPatientProfile(this.patientProfile).subscribe(
      (response) => {
        alert('Patient profile registered successfully!');
        this.router.navigate(['/admin/patient']);
        console.log('Success:', response);
      },
      (error) => {
        console.error('Error:', error);
        alert('Error registering Patient Profile: ' + (error.error || 'An unknown error occurred.'));
      }
    );
  }

}
