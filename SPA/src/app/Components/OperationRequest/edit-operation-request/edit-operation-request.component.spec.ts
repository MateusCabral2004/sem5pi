import {ComponentFixture, TestBed} from '@angular/core/testing';
import {EditOperationRequestComponent} from './edit-operation-request.component';

describe('EditOperationRequestComponent', () => {
  let component: EditOperationRequestComponent;
  let fixture: ComponentFixture<EditOperationRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditOperationRequestComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(EditOperationRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
