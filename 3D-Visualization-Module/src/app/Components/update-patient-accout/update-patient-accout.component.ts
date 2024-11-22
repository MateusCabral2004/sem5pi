import {Component, OnInit} from '@angular/core';
import {PatientService} from '../../services/PatientService/patient.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


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
      phonenumber: ['938536401', [Validators.pattern(/^\+?\d{1,3}[-\s]?\(?\d{1,4}\)?[-\s]?\d{3}[-\s]?\d{4}$/)]],
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
      console.log('Profile form:', this.profileForm.value);
      this.patientService.updateProfile(this.profileForm.value).subscribe({
        next: () => alert('Profile updated successfully.'),
        error: (err) => console.error('Error updating profile:', err)
      });
    } else {
      alert('Please fill in all required fields.');
    }
  }

}
