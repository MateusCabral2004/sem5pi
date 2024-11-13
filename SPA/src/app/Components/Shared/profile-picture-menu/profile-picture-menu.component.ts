import {Component, inject} from '@angular/core';
import {AuthService} from '../../../services/AuthService/auth.service';

@Component({
  selector: 'app-profile-menu',
  templateUrl: './profile-picture-menu.component.html',
  styleUrls: ['./profile-picture-menu.component.css']
})
export class ProfilePictureMenuComponent {
  auth = inject(AuthService);
  isMenuOpen = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  logout() {
    // Implement logout logic here
    this.auth.logout();
    this.isMenuOpen = false;
  }
}
