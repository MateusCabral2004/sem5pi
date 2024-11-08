import {Component, Inject} from '@angular/core';
import {AuthService} from '../../services/AuthService/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-staff-management',
  templateUrl: './staff-management.component.html',
  styleUrls: ['./staff-management.component.css']
})
export class StaffManagementComponent {
  private auth: AuthService;

  menuItems = [
    { title: 'View Staff Profiles', icon: 'assets/icons/dashboard.png', link: '/view' },
    { title: 'Create Staff Profile', icon: 'assets/icons/dashboard.png', link: '/add' },
    { title: 'Update Staff Profile', icon: 'assets/icons/dashboard.png', link: '/update' },
    { title: 'Delete Staff Profile', icon: 'assets/icons/dashboard.png', link: '/delete' },
  ];

  constructor(@Inject(AuthService) auth: AuthService, private router: Router) {
    this.auth = auth;
    this.validateUserRole();
  }

  private validateUserRole() {
    const expectedRole = "Admin";
    this.auth.validateUserRole(expectedRole);
  }
}

