import { Component } from '@angular/core';
import {PatientService} from '../../../services/PatientService/patient.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-check-user-to-delete',
  templateUrl: './check-user-to-delete.component.html',
  styleUrl: './check-user-to-delete.component.css',
  standalone: false
})
export class CheckUserToDeleteComponent {
  constructor(
    private patientService: PatientService,
    private router: Router
  ) {}
  checkUsersToDelete() {
    if (confirm('Are you sure you want to delete these accounts? This action is permanent.')) {
      this.patientService.checkUsersToDelete().subscribe(
        (response: any) => {
          alert(response.message);
        },
        error => {
          alert('Erro ao excluir conta: ' + error.error.message);
        }
      );
    }
  }
}
