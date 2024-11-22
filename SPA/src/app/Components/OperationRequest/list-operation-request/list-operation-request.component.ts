import {Component, OnInit} from '@angular/core';
import {OperationRequestService} from '../../../services/OperationRequestService/operation-request.service';

@Component({
  selector: 'app-list-operation-request',
  templateUrl: './list-operation-request.component.html',
  styleUrl: './list-operation-request.component.css',
  standalone: false
})
export class ListOperationRequestComponent  {
  searchFilters = {
    patientName: '',
    operationType: '',
    priority: '',
    status: ''
  };

  searchResults: any[] = [];


  constructor(private requisitionService: OperationRequestService) {}

  onSearch(): void {
    this.requisitionService.searchRequests(this.searchFilters).subscribe({
      next: (data) => {
        this.searchResults = data;
        console.log('Search Results:', this.searchResults);
      },
      error: (err) => {
        console.error('Error fetching data:', err);
      }
    });
  }
  deleteRequest(index: number): void {
    const requestToDelete = this.searchResults[index];

    const confirmDelete = window.confirm('Are you sure you want to delete this operation requisition?');

    if (confirmDelete) {
      this.requisitionService.deleteOperationRequest(requestToDelete.id).subscribe({
        next: () => {
          this.searchResults.splice(index, 1);  // Remove a requisição da lista
          console.log('Request deleted successfully.');
        },
        error: (err) => {
          console.error('Error deleting request:', err);
        }
      });
    } else {
      console.log('Delete action cancelled.');
    }
  }


}
