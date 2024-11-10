import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Staff } from '../../Components/staffManagement/Staff';
import { StaffIdDTO}  from '../../Components/staffManagement/StaffIdDTO';

@Injectable({
  providedIn: 'root'
})
export class StaffService {
  private apiUrl = 'http://localhost:5001';



  constructor(private http: HttpClient) {}

  listAllStaffProfiles(): Observable<Staff[]> {

    console.log()

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listAllStaffProfiles`, { withCredentials: true });
  }

  filterStaffProfilesByName(name: string): Observable<Staff[]> {

    const params = new HttpParams().set('name', name);

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listStaffProfilesByName`, { withCredentials: true, params });
  }

  filterStaffProfilesByEmail(email: string): Observable<any> {

    const params = new HttpParams().set('email', email);

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listStaffProfileByEmail`, { withCredentials: true, params });
  }

  filterStaffProfilesBySpecialization(specialization: string): Observable<Staff[]> {

    const params = new HttpParams().set('specialization', specialization);

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listStaffProfilesBySpecialization`, { withCredentials: true, params });
  }

  deleteStaffProfile(staffId: string): Observable<any> {

    const body = { Id: staffId };

    return this.http.patch(`${this.apiUrl}/Staff/deactivateStaffProfile`, body, { withCredentials: true });
  }

}
