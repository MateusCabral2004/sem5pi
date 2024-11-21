import { TestBed, fakeAsync } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('AuthService', () => {
  let service: AuthService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      providers: [
        AuthService,
        { provide: HttpClient, useValue: httpClientSpy },
        { provide: Router, useValue: routerSpy },
      ],
    });

    service = TestBed.inject(AuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getUserRole', () => {
    it('should return a user role on success', () => {
      const mockRole = 'admin';
      httpClientSpy.get.and.returnValue(of(mockRole));

      service.getUserRole().subscribe((role) => {
        expect(role).toBe(mockRole);
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        'http://localhost:5001/Login/role',
        { withCredentials: true, responseType: 'text' as 'json' }
      );
    });
  });

  describe('validateUserRole', () => {
    it('should navigate to root if the user role is not expected', fakeAsync(() => {
      httpClientSpy.get.and.returnValue(of('guest'));

      service.validateUserRole('admin', 'superuser');

      expect(routerSpy.navigate).toHaveBeenCalledWith(['/']);
    }));

    it('should not navigate if the user role is expected', fakeAsync(() => {
      httpClientSpy.get.and.returnValue(of('admin'));

      service.validateUserRole('admin', 'superuser');

      expect(routerSpy.navigate).not.toHaveBeenCalled();
    }));
  });
});
