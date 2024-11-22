import {Component, inject} from '@angular/core';
import {AuthService} from "../../../services/AuthService/auth.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: false
})
export class HomeComponent {
  authService = inject(AuthService)
  constructor() {
  }

  login() {
    this.authService.login()
  }

}
