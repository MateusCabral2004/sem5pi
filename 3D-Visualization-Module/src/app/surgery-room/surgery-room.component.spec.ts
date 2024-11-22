import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurgeryRoomComponent } from './surgery-room.component';

describe('SurgeryRoomComponent', () => {
  let component: SurgeryRoomComponent;
  let fixture: ComponentFixture<SurgeryRoomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SurgeryRoomComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SurgeryRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
