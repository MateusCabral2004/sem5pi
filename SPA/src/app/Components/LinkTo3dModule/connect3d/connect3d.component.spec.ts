import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Connect3dComponent } from './connect3d.component';

describe('Connect3dComponent', () => {
  let component: Connect3dComponent;
  let fixture: ComponentFixture<Connect3dComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Connect3dComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Connect3dComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
