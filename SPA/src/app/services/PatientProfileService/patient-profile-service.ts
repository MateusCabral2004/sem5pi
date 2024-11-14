import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class PatientProfileService {
  private apiUrl = 'http://localhost:5001/patientProfile';

  constructor(private http: HttpClient) {
  }

  registerPatientProfile(patient: PatientProfile): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/registerPatientProfile`, patient, {
      withCredentials: true,
    }).pipe(
      catchError(error => {
        console.error('Error registering the patient profile:', error);
        return of('');
      })
    );
  }

}
