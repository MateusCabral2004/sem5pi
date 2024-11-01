import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5001'; // Ensure this is the correct URL for your backend

  constructor(private http: HttpClient, private router: Router) {}

  login(): void {
    window.location.href = `${this.apiUrl}/Login/login`; // This will call the backend login method
  }
}
