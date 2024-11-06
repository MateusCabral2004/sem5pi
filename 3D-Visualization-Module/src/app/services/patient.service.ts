import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = 'http://localhost:5001/patient';

  constructor(private http: HttpClient) {}

  registerNumber(number: number): Observable<any> {
    const url = `${this.apiUrl}/register?number=${number}`;  
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    console.log('URL de requisição:', url);  
    return this.http.post(url, {}, { headers });  
  }
  
}
