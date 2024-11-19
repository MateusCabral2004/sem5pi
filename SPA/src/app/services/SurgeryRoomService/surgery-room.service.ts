import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SurgeryRoomService {

  private apiUrl = 'http://localhost:5001/surgeryRoom';

  constructor(private http: HttpClient) {
  }

  getSurgeryRooms(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/status`, {
      withCredentials: true
    }).pipe(
      catchError(error => {
        console.error('Error getting surgery rooms:', error);
        return of([]);
      })
    );
  }
}
