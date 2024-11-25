import { Component } from '@angular/core';

@Component({
  selector: 'app-patient-home',
  templateUrl: './patient-home.component.html',
  styleUrl: './patient-home.component.css'
})
export class PatientHomeComponent {
  menuItems = [
    { title: 'Update Profile Account', icon: 'assets/icons/dashboard.png', link: '/patient/updateAccount' },
    { title: 'Delete Account', icon: 'assets/icons/users.png', link: '/patient/deleteAccount' },
    { title: 'Patient Profile', icon: 'assets/icons/users.png', link: '/patient/profile' },
  ];
}
