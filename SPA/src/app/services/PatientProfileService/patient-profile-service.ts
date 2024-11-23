import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import json from '../../appsettings.json';



@Injectable({
  providedIn: 'root'
})

export class PatientProfileService {
  private apiUrl = json.apiUrl + '/Patient';

  constructor(private http: HttpClient) {
  }

  registerPatientProfile(patientProfile: PatientProfile): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/registerPatientProfile`, patientProfile, {
      withCredentials: true,
    }).pipe(
      catchError(error => {
        console.error('Error registering patient profile:', error);
        return of('');
      })
    );
  }

  deletePatientProfile(email: string): Observable<string> {
    return this.http.delete(`${this.apiUrl}/deletePatientProfile/${email}`, {
      withCredentials: true,
      responseType: 'text'
    }).pipe(
      catchError(error => {
        console.error('Error deleting patient profile:', error);
        return of('');
      })
    );
  }
  public listAllPatientProfiles(): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/`, { withCredentials: true });

  }

  public filterPatientProfilesByName(name: string): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/listPatientProfilesByName/${name}`, { withCredentials: true });

  }

  public filterPatientProfilesByEmail(email: string): Observable<any> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/listPatientProfilesByEmail/${email}`, { withCredentials: true });

  }

  public filterPatientProfilesByBirthDate(birthDate: string): Observable<PatientProfile[]> {

    return this.http.get<PatientProfile[]>(`${this.apiUrl}/Patient/listPatientProfilesByDateOfBirth/${birthDate}`, { withCredentials: true });

  }
  public filterPatientProfilesByMedicalRecordNumber(id: string): Observable<PatientProfile> {

    return this.http.get<PatientProfile>(`${this.apiUrl}/Patient/listPatientProfilesByMedicalRecordNumber/${id}`, {withCredentials: true});
  }

  public editPatientProfile(editedPatientProfile: PatientProfile, email:string): Observable<any> {

    return this.http.put(`${this.apiUrl}/Patient/editPatientProfile/${email}`, editedPatientProfile, { withCredentials: true });
  }
}
