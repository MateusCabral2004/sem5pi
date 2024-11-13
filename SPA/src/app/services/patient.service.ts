import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = 'http://localhost:5001/patient';

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

}
