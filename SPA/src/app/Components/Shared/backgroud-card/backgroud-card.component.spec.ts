import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BackgroudCardComponent } from './backgroud-card.component';

describe('BackgroudCardComponent', () => {
  let component: BackgroudCardComponent;
  let fixture: ComponentFixture<BackgroudCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BackgroudCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BackgroudCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
