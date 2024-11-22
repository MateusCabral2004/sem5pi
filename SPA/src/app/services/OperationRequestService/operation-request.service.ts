import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {
  private apiUrl = 'http://localhost:5001/staff'; // URL da API

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
      return this.http.get<any>(`${this.apiUrl}/search/requests/${normalizedFilters.patientName}/${normalizedFilters.operationType}/${normalizedFilters.priority}/${normalizedFilters.status}/`, {
      withCredentials: true
    });
  }

  deleteOperationRequest(operationId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/operations/requests/${operationId}`, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
}
