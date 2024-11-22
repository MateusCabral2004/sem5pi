import { Component } from '@angular/core';
import {PatientService} from "../../services/PatientService/patient.service";
import {Router} from "@angular/router";


@Component({
  selector: 'app-delete-patient-accout',
  standalone: false,
  templateUrl: './delete-patient-accout.component.html',
  styleUrl: './delete-patient-accout.component.css'
})
export class DeletePatientAccoutComponent {
  constructor(
      private patientService: PatientService,
      private router: Router
  ) {}

  deleteAccount() :void{
    if (confirm("Are you sure you want to delete your account? This action is permanent.")) {
      this.patientService.deleteAccount().subscribe(
          response => {
            alert(response);
          },
          error => {
            alert("Erro ao excluir conta: " + error.error.message);
          }
      );
    }
  }
}
