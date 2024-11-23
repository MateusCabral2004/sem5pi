import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatePatientAccoutComponent } from './update-patient-accout.component';

describe('UpdatePatientAccoutComponent', () => {
  let component: UpdatePatientAccoutComponent;
  let fixture: ComponentFixture<UpdatePatientAccoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdatePatientAccoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatePatientAccoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
