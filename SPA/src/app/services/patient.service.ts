import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import json from '../appsettings.json';


@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl =  json.apiUrl + '/patient';

  constructor(private http: HttpClient) {
  }
  deleteAccount(): Observable<any> {
    const url = `${this.apiUrl}/account/exclude`;

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.delete(url, {
      withCredentials: true,
      responseType: 'json',
      headers: headers
    });
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

}
