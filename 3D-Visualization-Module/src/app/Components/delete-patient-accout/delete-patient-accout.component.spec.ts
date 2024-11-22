import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePatientAccoutComponent } from './delete-patient-accout.component';

describe('DeletePatientAccoutComponent', () => {
  let component: DeletePatientAccoutComponent;
  let fixture: ComponentFixture<DeletePatientAccoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeletePatientAccoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeletePatientAccoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
