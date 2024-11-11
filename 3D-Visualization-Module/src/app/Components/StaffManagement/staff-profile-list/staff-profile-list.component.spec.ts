import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffProfileListComponent } from './staff-profile-list.component';

describe('StaffProfileListComponent', () => {
  let component: StaffProfileListComponent;
  let fixture: ComponentFixture<StaffProfileListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StaffProfileListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffProfileListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
