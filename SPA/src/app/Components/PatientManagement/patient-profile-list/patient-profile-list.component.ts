import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PatientsListing} from '../../../Domain/PatientsListing';


@Component({
  selector: 'app-patient-profile-list',
  templateUrl: './patient-profile-list.component.html',
  styleUrl: './patient-profile-list.component.css'
})
export class PatientProfileListComponent {
  @Input() patientProfileList: PatientsListing[] = [];
  @Output() deletePatientProfile = new EventEmitter<string>();
  @Output() editPatientProfile = new EventEmitter<string>();

  ngOnInit(){
    console.log(this.patientProfileList);
  }
}
