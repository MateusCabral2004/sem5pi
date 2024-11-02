import {Component, Inject} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-operation-type-management',
  templateUrl: './operation-type-management.component.html',
  styleUrls: ['./operation-type-management.component.css']
})
export class OperationTypeManagementComponent {
  private auth: AuthService;

  menuItems = [
    { title: 'Add Operation Type', icon: 'assets/icons/dashboard.png', link: '/add' },
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

