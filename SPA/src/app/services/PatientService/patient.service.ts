import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import json from '../../appsettings.json';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl =  json.apiUrl + '/patient';

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

  updateProfile(profileData: any): Observable<any> {
    return this.http.patch(`${this.apiUrl}/account/update`, JSON.stringify(profileData), {
        withCredentials: true,
        responseType: 'arraybuffer',
        headers: { 'Content-Type': 'application/json' }
    });
  }


}
