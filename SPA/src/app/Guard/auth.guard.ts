import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/AuthService/auth.service';
import {catchError, map, Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const expectedRoles: string[] = route.data['roles'];

    return this.authService.getUserRole().pipe(
      map(userRole => {
        if (!userRole || (expectedRoles && !expectedRoles.includes(userRole.toLowerCase()))) {
          this.router.navigate(['/']);
          return false;
        }
        return true;
      }),
      catchError((err) => {
        console.error('Error fetching user role:', err);
        this.router.navigate(['/']);
        return of(false);
      })
    );
  }
}
