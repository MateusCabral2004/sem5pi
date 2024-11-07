import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOperationTypeComponent } from './view-operation-type.component';

describe('ViewOperationTypeComponent', () => {
  let component: ViewOperationTypeComponent;
  let fixture: ComponentFixture<ViewOperationTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewOperationTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewOperationTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
