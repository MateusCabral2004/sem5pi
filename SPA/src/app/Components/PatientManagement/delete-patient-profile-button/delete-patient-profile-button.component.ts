import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-delete-patient-profile-button',
  templateUrl: './delete-patient-profile-button.component.html',
  styleUrl: './delete-patient-profile-button.component.css',
  standalone: false

})

export class DeletePatientProfileButtonComponent {
  @Input() patientId!: string;
  @Output() delete = new EventEmitter<string>();

  onDelete() {
    this.delete.emit(this.patientId);
  }
}
