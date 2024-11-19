import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import {CreateStaff} from '../../Domain/CreateStaff';
import {Staff} from '../../Domain/Staff';
import {OperationType} from '../../Domain/OperationType';
import {PatientsListing} from '../../Domain/PatientsListing';

@Injectable({
  providedIn: 'root'
})

export class PatientProfileService {
  private apiUrl = 'http://localhost:5001';

  constructor(private http: HttpClient) {
  }

  registerPatientProfile(patientProfile: PatientProfile): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/Patient/registerPatientProfile`, patientProfile, {
      withCredentials: true,
    }).pipe(
      catchError(error => {
        console.error('Error registering patient profile:', error);
        return of('');
      })
    );
  }

  deletePatientProfile(patientProfile: PatientProfile): Observable<string> {
    return this.http.delete(`${this.apiUrl}/deletePatientProfile/${patientProfile.email}`, {
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

    return this.http.get<PatientsListing[]>(`${this.apiUrl}/Patient`, { withCredentials: true });
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

}
