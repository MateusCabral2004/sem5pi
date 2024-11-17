import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import {CreateStaff} from '../../Domain/CreateStaff';

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

}
