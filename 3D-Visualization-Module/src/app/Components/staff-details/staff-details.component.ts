import {Component, Input} from '@angular/core';
import {Staff} from "../staffManagement/Staff";

@Component({
  selector: 'app-staff-details',
  templateUrl: './staff-details.component.html',
  styleUrl: './staff-details.component.css'
})
export class StaffDetailsComponent {
  @Input() staff!: Staff;
}
