import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Staff} from '../../../Domain/Staff';

@Component({
  selector: 'app-staff-profile-list',
  templateUrl: './staff-profile-list.component.html',
  styleUrl: './staff-profile-list.component.css'
})
export class StaffProfileListComponent {
  @Input() staffList: Staff[] = [];
  @Output() deleteStaffProfile = new EventEmitter<string>();
  @Output() editStaffProfile = new EventEmitter<Staff>();
}
