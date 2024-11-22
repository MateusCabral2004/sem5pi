import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrl: './confirm-modal.component.css'
})
export class ConfirmModalComponent {
  isVisible: boolean = false;
  message: string = '';

  @Output() confirmed: EventEmitter<boolean> = new EventEmitter();

  open(message: string) {
    this.message = message;
    this.isVisible = true;
  }

  close() {
    this.isVisible = false;
  }

  confirm() {
    this.confirmed.emit(true);
    this.close();
  }

  cancel() {
    this.confirmed.emit(false);
    this.close();
  }
}
