import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-edit-staff-profile-button',
  templateUrl: './edit-staff-profile-button.component.html',
  styleUrl: './edit-staff-profile-button.component.css',
  standalone: false
})
export class EditStaffProfileButtonComponent {
  @Input() staffId!: string;
  @Output() edit = new EventEmitter<string>();

  onEdit() {
    this.edit.emit(this.staffId);
  }
}
