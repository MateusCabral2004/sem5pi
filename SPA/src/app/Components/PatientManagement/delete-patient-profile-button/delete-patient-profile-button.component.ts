import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-delete-patient-profile-button',
  templateUrl: './delete-patient-profile-button.component.html',
  styleUrl: './delete-staff-patient-button.component.css'
})

export class DeletePatientProfileButtonComponent {
  @Input() patientId!: string;
  @Output() delete = new EventEmitter<string>();

  onDelete() {
    this.delete.emit(this.patientId);
  }
}
