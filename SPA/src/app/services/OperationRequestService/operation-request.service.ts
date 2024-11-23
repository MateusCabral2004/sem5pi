
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {OperationRequest} from '../../Domain/OperationRequest';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import json from '../../appsettings.json';


@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  private apiUrl1 = json.apiUrl + '/staff';
  private apiUrl = json.apiUrl + '/operationRequest';

  constructor(private http: HttpClient) {}

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

  addOperationRequest(operation: OperationRequest): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/registerOperationRequest`, operation, {
      withCredentials: true,
    }).pipe(
      catchError(error => {
        console.error('Error adding operation request:', error);
        return of('');
      })
    );
  } //corrigir


  listOperationRequests(): Observable<OperationRequest[]> {
    return this.http.get<OperationRequest[]>(`${this.apiUrl}`, {withCredentials: true});
  }

}
