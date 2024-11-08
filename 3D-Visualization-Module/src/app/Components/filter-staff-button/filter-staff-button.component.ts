import { Component, ElementRef, EventEmitter, Output, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-filter-staff-button',
  templateUrl: './filter-staff-button.component.html',
  styleUrls: ['./filter-staff-button.component.css']
})
export class FilterStaffButtonComponent {

  @Output() filterSelected = new EventEmitter<string>();
  selectedFilter: string = '';
  showOptions = false;
  filters = ['Name', 'Email', 'Specialization'];

  private clickListener: () => void;

  constructor(private renderer: Renderer2, private elRef: ElementRef) {
    this.clickListener = this.renderer.listen('document', 'click', this.handleClickOutside.bind(this));
  }

  toggleFilterOptions() {
    this.showOptions = !this.showOptions;
  }

  selectFilter(filter: string) {
    this.selectedFilter = filter;
    this.filterSelected.emit(filter);
    this.showOptions = false;
  }

  handleClickOutside(event: Event) {
    if (!this.elRef.nativeElement.contains(event.target)) {
      this.showOptions = false;
    }
  }

}
