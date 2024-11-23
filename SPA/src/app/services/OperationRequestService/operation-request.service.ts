
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

  private apiUrl1 = 'http://localhost:5001/staff';

  constructor(private http: HttpClient) {}

  // Método para buscar as requisições de operação
  searchRequests(filters: any): Observable<any> {
    const { patientName, operationType, priority, status } = filters;
    const normalizedFilters = {
      patientName: patientName === '' ? null : patientName,
      operationType: operationType === '' ? null : operationType,
      priority: priority === '' ? null : priority,
      status: status === '' ? null : status
    };
    console.log('Filters:', filters);
      return this.http.get<any>(`${this.apiUrl1}/search/requests/${normalizedFilters.patientName}/${normalizedFilters.operationType}/${normalizedFilters.priority}/${normalizedFilters.status}/`, {
      withCredentials: true
    });
  }

  deleteOperationRequest(operationId: string): Observable<any> {
    console.log('Deleting operation request:', operationId);
    return this.http.delete(`${this.apiUrl1}/request/deleteRequest/${operationId}`, {
      withCredentials: true
      }
    );
  }

  private apiUrl = 'http://localhost:5001';

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
