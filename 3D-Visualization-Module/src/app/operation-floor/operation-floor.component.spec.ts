import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationFloorComponent } from './operation-floor.component';

describe('OperationFloorComponent', () => {
  let component: OperationFloorComponent;
  let fixture: ComponentFixture<OperationFloorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OperationFloorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OperationFloorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
