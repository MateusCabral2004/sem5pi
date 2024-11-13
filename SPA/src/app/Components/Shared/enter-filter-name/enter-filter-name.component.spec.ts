import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterFilterNameComponent } from './enter-filter-name.component';

describe('EnterFilterNameComponent', () => {
  let component: EnterFilterNameComponent;
  let fixture: ComponentFixture<EnterFilterNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnterFilterNameComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnterFilterNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
