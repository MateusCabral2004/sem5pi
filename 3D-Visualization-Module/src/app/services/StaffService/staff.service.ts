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

  public listAllStaffProfiles(): Observable<Staff[]> {

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listAllStaffProfiles`, { withCredentials: true });
  }

  public filterStaffProfilesByName(name: string): Observable<Staff[]> {

    const params = new HttpParams().set('name', name);

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listStaffProfilesByName`, { withCredentials: true, params });
  }

  public filterStaffProfilesByEmail(email: string): Observable<any> {

    const params = new HttpParams().set('email', email);

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listStaffProfileByEmail`, { withCredentials: true, params });
  }

  public filterStaffProfilesBySpecialization(specialization: string): Observable<Staff[]> {

    const params = new HttpParams().set('specialization', specialization);

    return this.http.get<Staff[]>(`${this.apiUrl}/Staff/listStaffProfilesBySpecialization`, { withCredentials: true, params });
  }

  public deleteStaffProfile(staffId: string): Observable<any> {

    const body = { Id: staffId };

    return this.http.patch(`${this.apiUrl}/Staff/deactivateStaffProfile`, body, { withCredentials: true });
  }

  public updateStaffProfile(updatedStaffProfile: Staff): Observable<any> {

    return this.http.patch(`${this.apiUrl}/Staff/editStaffProfile`, updatedStaffProfile, { withCredentials: true });
  }

}
