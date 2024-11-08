import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-enter-filter-name',
  templateUrl: './enter-filter-name.component.html',
  styleUrl: './enter-filter-name.component.css'
})
export class EnterFilterNameComponent {

  @Output() nameToFilter = new EventEmitter<string>();

  isVisible: boolean = false;
  nameInput: string = '';

  open() {
    this.isVisible = true;
  }

  confirmName() {
    this.nameToFilter.emit(this.nameInput);
    this.isVisible = false;
  }

  close() {
    this.isVisible = false;
  }
}
