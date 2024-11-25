import {Component, OnInit} from '@angular/core';
import {PatientService} from '../../../services/PatientService/patient.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-patient-profile',
  templateUrl: './patient-profile.component.html',
  styleUrl: './patient-profile.component.css',
  standalone: false
})
export class PatientProfileComponent implements OnInit{
  patientData={
    name: null,
    birthDate: null,
    allergies: null,
    phoneNumber: null,
    email: null,
    appointmentHistory: null,
    emergencyContact: null,
    gender: null

  } // Armazenará os dados do paciente

  constructor(protected router:Router, private patientService:PatientService) {}
  ngOnInit(): void {
    this.loadPatientData();
  }

  loadPatientData(): void {
    this.patientService.getProfile().subscribe(
      (data) => {
        console.log('Dados do paciente:', data);
        this.patientData = data[0];
        console.log('Dados do paciente 0:', data[0]);

        console.log('Dados do paciente:', this.patientData);

      },
      (error) => {
        console.error('Erro ao carregar os dados do paciente:', error);
      }
    );
  }

}
