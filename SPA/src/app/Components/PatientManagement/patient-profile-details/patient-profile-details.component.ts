import {Component, Input} from '@angular/core';
import {PatientProfile} from '../../../Domain/PatientProfile';
import {PatientsListing} from '../../../Domain/PatientsListing';

@Component({
  selector: 'app-patient-profile-details',
  templateUrl: './patient-profile-details.component.html',
  styleUrl: './patient-profile-details.component.css',
  standalone: false
})
export class PatientProfileDetailsComponent {
  @Input() patient!: PatientsListing;
}
