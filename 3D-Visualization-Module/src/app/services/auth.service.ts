import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5001';

  constructor(private http: HttpClient, private router: Router) {
  }

  login(): void {
    window.location.href = `${this.apiUrl}/Login/login`;
  }

  logout(): void {
    window.location.href = `${this.apiUrl}/Login/logout`;
  }

  isAuthenticated(): void {
    //implement this method in the backend
  }

  getRole(): void {
    //implement this method in the backend
  }


}
