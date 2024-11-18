import {Component, Input} from '@angular/core';
import {PatientProfile} from '../../../Domain/PatientProfile';

@Component({
  selector: 'app-patient-profile-details',
  templateUrl: './patient-profile-details.component.html',
  styleUrl: './patient-profile-details.component.css'
})
export class PatientProfileDetailsComponent {
  @Input() patient!: PatientProfile;
}
