import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationTypeManagementComponent } from './operation-type-management.component';

describe('OperationTypeManagementComponent', () => {
  let component: OperationTypeManagementComponent;
  let fixture: ComponentFixture<OperationTypeManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OperationTypeManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OperationTypeManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
