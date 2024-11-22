import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-edit-patient-profile-button',
  templateUrl: './edit-patient-profile-button.component.html',
  styleUrl: './edit-patient-profile-button.component.css'
})
export class EditPatientProfileButtonComponent {
  @Input() patientId!: string;
  @Output() edit = new EventEmitter<string>();

  onEdit() {
    this.edit.emit(this.patientId);
  }
}
