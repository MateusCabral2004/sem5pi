import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import {CreateStaff} from '../../Domain/CreateStaff';
import {Staff} from '../../Domain/Staff';
import {OperationType} from '../../Domain/OperationType';

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

  public filterPatientProfilesByName(name: string): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/by-name/${name}`, { withCredentials: true });
  }

  public filterPatientProfilesByEmail(email: string): Observable<any> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/by-email/${email}`, { withCredentials: true });
  }

  public filterPatientProfilesByBirthDate(birthDate: string): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/by-birthDate/${birthDate}`, { withCredentials: true });
  }
  public filterPatientProfilesByMedicalRecordNumber(medicalRecordNumber: string): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/by-birthDate/${medicalRecordNumber}`, { withCredentials: true });
  }

  public editPatientProfile(editedPatientProfile: PatientProfile): Observable<any> {

    return this.http.patch(`${this.apiUrl}/Patient`, editedPatientProfile, { withCredentials: true });
  }

  listPatientProfiles(): Observable<PatientProfile[]> {
    return this.http.get<PatientProfile[]>(`${this.apiUrl}/listPatientProfiles`, {withCredentials: true}).pipe(
      catchError(error => {
        console.error('Failed to load patient profiles:', error);
        return of([]);
      })
    );
  }

  filterPatientProfiles(currentFilter: string, filterValue: string): Observable<PatientProfile[]> {
    return this.http.get<PatientProfile[]>(`${this.apiUrl}/listPatientProfilesBy${currentFilter}/${filterValue}`, {withCredentials: true}).pipe(
      catchError(error => {
        console.error('Failed to load patient profiles:', error);
        return of([]);
      })
    );
  }
}
