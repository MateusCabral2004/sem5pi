import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import {OperationType} from '../../Domain/OperationType';
import {RequiredStaff} from "../../Domain/RequiredStaff";

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

    removeRequiredStaff(operation: OperationType, requiredStaff: RequiredStaff): Observable<string> {
        return this.http.put(`${this.apiUrl}/editOperationType/requiredStaff/remove/${encodeURIComponent(operation.operationName)}`, JSON.stringify(requiredStaff.specialization), {
            withCredentials: true,
            responseType: 'text',
            headers: { 'Content-Type': 'application/json' }
        }).pipe(
            tap(response => {
                console.log('Remove required staff response:', response);
            }),
            catchError(error => {
                console.error('Error removing required staff:', error);
                return of('');
            })
        );
    }


    addRequiredStaff(operation: OperationType, requiredStaff: RequiredStaff): Observable<string> {
        return this.http.put(`${this.apiUrl}/editOperationType/requiredStaff/add/${encodeURIComponent(operation.operationName)}`, requiredStaff, {
            withCredentials: true,
            responseType: 'text'
        }).pipe(
            tap(response => {
                console.log('Add required staff response:', response);
            }),
            catchError(error => {
                console.error('Error adding required staff:', error);
                return of('');
            })
        );
    }

    updateDuration(name: string, duration: string, typeOfDuration: string): Observable<string> {
        const sanitizedDuration = duration.replace(/^0+/, '');

        return this.http.put(`${this.apiUrl}/editOperationType/duration/${typeOfDuration}/${encodeURIComponent(name)}`, JSON.stringify(sanitizedDuration), {
            withCredentials: true,
            responseType: 'text',
            headers: { 'Content-Type': 'application/json' }
        }).pipe(
            tap(response => {
                console.log('Update duration response:', response);
            }),
            catchError(error => {
                console.error('Error updating duration:', error);
                return of('');
            })
        );
    }

    updateOperationName(originalName: string, newName: string): Observable<string> {
        return this.http.put(`${this.apiUrl}/editOperationType/name/${encodeURIComponent(originalName)}`, JSON.stringify(newName), {
            withCredentials: true,
            responseType: 'text',
            headers: { 'Content-Type': 'application/json' }
        }).pipe(
            tap(response => {
                console.log('Update operation name response:', response);
            }),
            catchError(error => {
                console.error('Error updating operation name:', error);
                return of('');
            })
        );
    }


}
