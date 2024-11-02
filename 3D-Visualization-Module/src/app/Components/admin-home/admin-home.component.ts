import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminMenuComponent {
  menuItems = [
    /* The menus are temporary and will need to be replaced with the actual menu items
       but because they are not yet implemented i created some placeholders */
    { title: 'Dashboard', icon: 'assets/icons/dashboard.png', link: '/dashboard' },
    { title: 'Users', icon: 'assets/icons/users.png', link: '/users' },
    { title: 'Reports', icon: 'assets/icons/reports.png', link: '/reports' },
    { title: 'Settings', icon: 'assets/icons/settings.png', link: '/settings' },
    { title: 'Profile', icon: 'assets/icons/profile.png', link: '/profile' }
  ];
}
