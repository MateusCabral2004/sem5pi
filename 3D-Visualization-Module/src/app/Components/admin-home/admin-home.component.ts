import {Component, Inject} from '@angular/core';
import {AuthService} from '../../services/AuthService/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminMenuComponent {
  private auth: AuthService;

  menuItems = [
    /* The menus are temporary and will need to be replaced with the actual menu items
       but because they are not yet implemented i created some placeholders */
    { title: 'Operation Type Management', icon: 'assets/icons/dashboard.png', link: '/admin/operationTypeManagement' },
    { title: 'Staff Management', icon: 'assets/icons/users.png', link: '/admin/staff' },
    { title: 'Reports', icon: 'assets/icons/reports.png', link: '/reports' },
    { title: 'Settings', icon: 'assets/icons/settings.png', link: '/settings' },
    { title: 'Profile', icon: 'assets/icons/profile.png', link: '/profile' }
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

