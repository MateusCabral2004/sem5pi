import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {OperationRequest} from '../../Domain/OperationRequest';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {PatientProfile} from '../../Domain/PatientProfile';
import {Staff} from '../../Domain/Staff';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {
  private apiUrl = 'http://localhost:5001';

  constructor(private http: HttpClient) {
  }

  addOperationRequest(operation: OperationRequest): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/OperationRequest/registerOperationRequest`, operation, {
      withCredentials: true,
    }).pipe(
      catchError(error => {
        console.error('Error adding operation request:', error);
        return of('');
      })
    );
  } //corrigir


  listOperationRequests(): Observable<OperationRequest[]> {
    return this.http.get<OperationRequest[]>(`${this.apiUrl}/OperationRequest`, {withCredentials: true});
  }



}
