import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-enter-filter-name',
  templateUrl: './enter-filter-name.component.html',
  styleUrl: './enter-filter-name.component.css',
  standalone: false
})
export class EnterFilterNameComponent {

  @Output() nameToFilter = new EventEmitter<string>();
  @Output() filterTypeToEmit  = new EventEmitter<string>();

  isVisible: boolean = false;
  nameInput: string = '';
  filterType: string = '';

  open(filterType: string) {
    this.nameInput = '';
    this.filterType = filterType;
    this.isVisible = true;
  }

  confirmName() {
    this.filterTypeToEmit.emit(this.filterType);
    this.nameToFilter.emit(this.nameInput);
    this.isVisible = false;
  }

  close() {
    this.isVisible = false;
  }
}
