import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {PatientService} from '../../../services/PatientService/patient.service';

@Component({
  selector: 'app-update-patient-accout',
  standalone: false,
  templateUrl: './update-patient-accout.component.html',
  styleUrl: './update-patient-accout.component.css'
})
export class UpdatePatientAccoutComponent  {
  profileForm: FormGroup;

  constructor(private fb: FormBuilder, private patientService: PatientService) {
    this.profileForm = this.fb.group({
      email: ['sandroluis720@gmail.com', [Validators.email]],
      phoneNumber: ['938536401', [Validators.pattern(/^\+?\d{1,3}[-\s]?\(?\d{1,4}\)?[-\s]?\d{3}[-\s]?\d{4}$/)]],
      firstName: ['cocielo'],
      lastName: ['updateLuis'],
      birthDate: ['1985-10-12'],
      gender: ['Male'],
      allergiesAndMedicalConditions: ['Penicillin, Asthma'],
      emergencyContact: ['Jane Doe - 987654321'],
      appointmentHistory: ['2023-05-15, 2024-03-12']
    });
  }

  updateProfile(): void {

    if (this.profileForm.valid) {
      const patientDto: {
        firstName: null;
        lastName: null;
        allergiesAndMedicalConditions: any;
        phoneNumber: null;
        gender: null;
        appointmentHistory: any;
        emergencyContact: null;
        birthDate: null;
        email: null
      } = {
        email: this.profileForm.value.email || null,
        phoneNumber: this.profileForm.value.phoneNumber || null,
        firstName: this.profileForm.value.firstName || null,
        lastName: this.profileForm.value.lastName || null,
        birthDate: this.profileForm.value.birthDate || null,
        gender: this.profileForm.value.gender || null,
        allergiesAndMedicalConditions: this.profileForm.value.allergiesAndMedicalConditions
          ? this.profileForm.value.allergiesAndMedicalConditions.split(', ')
          : null,
        emergencyContact: this.profileForm.value.emergencyContact || null,
        appointmentHistory: this.profileForm.value.appointmentHistory
          ? this.profileForm.value.appointmentHistory.split(', ')
          : null
      };
      console.log('Profile form:', patientDto);
      this.patientService.updateProfile(patientDto).subscribe({
        next: () => alert('Profile updated successfully.'),
        error: (err) => console.error('Error updating profile:', err)
      });
    } else {
      alert('Please fill in all required fields.');
    }
  }

}
