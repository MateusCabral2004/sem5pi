import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdgeWallComponent } from './edge-wall.component';

describe('EdgeWallComponent', () => {
  let component: EdgeWallComponent;
  let fixture: ComponentFixture<EdgeWallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EdgeWallComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EdgeWallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
