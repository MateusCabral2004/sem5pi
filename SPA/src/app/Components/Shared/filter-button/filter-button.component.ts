import {Component, ElementRef, EventEmitter, Input, Output, Renderer2} from '@angular/core';

@Component({
  selector: 'app-filter-button',
  templateUrl: './filter-button.component.html',
  styleUrls: ['./filter-button.component.css'],
  standalone: false
})
export class FilterButtonComponent {

  @Input() filters: string[] = [] //['Name', 'Email', 'Specialization'];
  @Output() filterSelected = new EventEmitter<string>();
  selectedFilter: string = '';
  showOptions = false;

  private clickListener: () => void;

  constructor(private renderer: Renderer2, private elRef: ElementRef) {
    this.clickListener = this.renderer.listen('document', 'click', this.handleClickOutside.bind(this));
    console.log('FilterButtonComponent created');
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
