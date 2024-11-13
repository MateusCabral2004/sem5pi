import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Staff } from '../../Domain/Staff';
import {CreateStaff} from '../../Domain/CreateStaff';

@Injectable({
  providedIn: 'root'
})
export class StaffService {
  private apiUrl = 'http://localhost:5001';

  constructor(private http: HttpClient) {}

  public listAllStaffProfiles(): Observable<Staff[]> {

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff`, { withCredentials: true });
  }

  public filterStaffProfilesByName(name: string): Observable<Staff[]> {

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/by-name/${name}`, { withCredentials: true });
  }

  public filterStaffProfilesByEmail(email: string): Observable<any> {

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/by-email/${email}`, { withCredentials: true });
  }

  public filterStaffProfilesBySpecialization(specialization: string): Observable<Staff[]> {

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/by-specialization/${specialization}`, { withCredentials: true });
  }

  public deleteStaffProfile(staffId: string): Observable<any> {

    return this.http.delete(`${this.apiUrl}/Staff/${staffId}`, { withCredentials: true });
  }

  public updateStaffProfile(updatedStaffProfile: Staff): Observable<any> {

    return this.http.patch(`${this.apiUrl}/Staff`, updatedStaffProfile, { withCredentials: true });
  }

  public createStaffProfile(newStaffProfile: CreateStaff): Observable<any> {

    return this.http.post(`${this.apiUrl}/Staff`, newStaffProfile, { withCredentials: true });
  }

}
