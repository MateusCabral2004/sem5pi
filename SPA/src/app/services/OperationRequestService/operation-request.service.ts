import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  private apiUrl = 'http://localhost:5001/staff'; // URL da API

  constructor(private http: HttpClient) {
  }
  searchRequests(filters: any): Observable<any> {
    const params = {
      filters
    };
    console.log('params', params);
    return this.http.get<any>(`${this.apiUrl}/search/requests`, {withCredentials:true });
  }

  getOperationRequests(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/operations/requests`);
  }

  deleteOperationRequest(operationId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/operations/requests/${operationId}`, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

}
