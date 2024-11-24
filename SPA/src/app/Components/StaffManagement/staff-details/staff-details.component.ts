import {Component, Input} from '@angular/core';
import {Staff} from "../../../Domain/Staff";

@Component({
  selector: 'app-staff-details',
  templateUrl: './staff-details.component.html',
  styleUrl: './staff-details.component.css',
  standalone: false
})
export class StaffDetailsComponent {
  @Input() staff!: Staff;
}
