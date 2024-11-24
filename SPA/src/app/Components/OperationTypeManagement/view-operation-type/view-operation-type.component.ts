import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OperationType } from '../../../Domain/OperationType';

@Component({
  selector: 'app-operation-type-view',
  templateUrl: './view-operation-type.component.html',
  styleUrls: ['./view-operation-type.component.css'],
  standalone: false
})
export class ViewOperationTypeComponent implements OnInit {
  operation!: OperationType;

  constructor(private route: ActivatedRoute, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.operation = navigation?.extras?.state?.['operation'];
  }

  ngOnInit(): void {
    if (!this.operation) {
      this.router.navigate(['/admin/operationTypeManagement']);
    }
  }
}
