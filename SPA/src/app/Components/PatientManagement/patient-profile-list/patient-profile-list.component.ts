import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PatientProfile} from '../../../Domain/PatientProfile';


@Component({
  selector: 'app-patient-profile-list',
  templateUrl: './patient-profile-list.component.html',
  styleUrl: './patient-profile-list.component.css',
  standalone: false
})
export class PatientProfileListComponent {
  @Input() patientProfileList: PatientProfile[] = [];
  @Output() deletePatientProfile = new EventEmitter<string>();
  @Output() editPatientProfile = new EventEmitter<PatientProfile>();

}
