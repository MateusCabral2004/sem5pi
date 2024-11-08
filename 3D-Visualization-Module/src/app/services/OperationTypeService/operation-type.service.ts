import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { OperationType } from '../../Domain/OperationType';

@Injectable({
  providedIn: 'root',
})
export class OperationTypeService {
  private apiUrl = 'http://localhost:5001/operationType';

  constructor(private http: HttpClient) {
  }

  listOperationTypes(): Observable<OperationType[]> {
    return this.http.get<OperationType[]>(`${this.apiUrl}/listOperationType`, {withCredentials: true}).pipe(
      tap(data => {
        console.log('Operation types fetched:', data);
      }),
      catchError(error => {
        console.error('Failed to load operation types:', error);
        return of([]);
      })
    );
  }

  addOperationType(operation: OperationType): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/addNewOperationType`, operation, {
      withCredentials: true,
    }).pipe(
      catchError(error => {
        console.error('Error adding operation type:', error);
        return of('');
      })
    );
  }

  deleteOperationType(operation: OperationType): Observable<string> {
    return this.http.delete(`${this.apiUrl}/deleteOperationType/${operation.operationName}`, {
      withCredentials: true,
      responseType: 'text'
    }).pipe(
      tap(response => {
        console.log('Delete operation response:', response);
      }),
      catchError(error => {
        console.error('Error deleting operation type:', error);
        return of('');
      })
    );
  }
}
