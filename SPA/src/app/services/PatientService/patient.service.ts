import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import json from '../../appsettings.json';
import {PatientProfile} from '../../Components/Patient/class/PatientProfile';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = json.apiUrl + '/patient';

  constructor(private http: HttpClient) {
  }

  registerNumber(number: number): Observable<any> {
    const url = `${this.apiUrl}/register`;

    console.log('URL de requisição:', url);
    return this.http.post(url, null, {
      responseType: 'arraybuffer',
      withCredentials: true,
      params: {number: number}
    });
  }

  updateProfile(profileData: {
    firstName: null;
    lastName: null;
    allergiesAndMedicalConditions: any;
    phoneNumber: null;
    gender: null;
    appointmentHistory: any;
    emergencyContact: null;
    birthDate: null;
    email: null
  }): Observable<any> {
    console.log('Profile data:', profileData);
    return this.http.patch(`${this.apiUrl}/account/update`, profileData, {
        withCredentials: true
      }
    );
  }


}
