import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-delete-staff-profile-button',
  templateUrl: './delete-staff-profile-button.component.html',
  styleUrl: './delete-staff-profile-button.component.css'
})

export class DeleteStaffProfileButtonComponent {
  @Input() staffId!: string;
  @Output() delete = new EventEmitter<string>();

  onDelete() {
    this.delete.emit(this.staffId);
  }
}
