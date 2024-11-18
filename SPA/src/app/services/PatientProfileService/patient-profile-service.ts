import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import {CreateStaff} from '../../Domain/CreateStaff';
import {Staff} from '../../Domain/Staff';

@Injectable({
  providedIn: 'root'
})

export class PatientProfileService {
  private apiUrl = 'http://localhost:5001';

  constructor(private http: HttpClient) {
  }

  public registerPatientProfile(newPatientProfile: PatientProfile): Observable<any> {

    return this.http.post(`${this.apiUrl}/Patient`, newPatientProfile, { withCredentials: true });
  }

  public deletePatientProfile(patientId: string): Observable<any> {

    return this.http.delete(`${this.apiUrl}/Patient/${patientId}`, { withCredentials: true });
  }
  public listAllPatientProfiles(): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient`, { withCredentials: true });
  }
}
