import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {PatientsListing} from '../../Domain/PatientsListing';
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
  public listAllPatientProfiles(): Observable<PatientsListing[]> {

    return this.http.get<PatientsListing[]>(`${this.apiUrl}`, { withCredentials: true });
  }

  public filterPatientProfilesByName(name: string): Observable<PatientsListing[]> {

    return this.http.get<PatientsListing[]>(`${this.apiUrl}/listPatientProfilesByName/${name}`, { withCredentials: true });
  }

  public filterPatientProfilesByEmail(email: string): Observable<any> {

    return this.http.get<PatientsListing[]>(`${this.apiUrl}/listPatientProfilesByEmail/${email}`, { withCredentials: true });
  }

  public filterPatientProfilesByBirthDate(birthDate: string): Observable<PatientsListing[]> {

    return this.http.get<PatientsListing[]>(`${this.apiUrl}/listPatientProfilesByDateOfBirth/${birthDate}`, { withCredentials: true });
  }
  public filterPatientProfilesByMedicalRecordNumber(id: string): Observable<PatientsListing> {

    return this.http.get<PatientsListing>(`${this.apiUrl}/listPatientProfilesByMedicalRecordNumber/${id}`, { withCredentials: true });
  }

  public editPatientProfile(editedPatientProfile: PatientProfile): Observable<any> {

    return this.http.patch(`${this.apiUrl}`, editedPatientProfile, { withCredentials: true });
  }
}
