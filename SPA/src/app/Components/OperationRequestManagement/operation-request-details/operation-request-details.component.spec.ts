import {OperationRequestDetailsComponent} from './operation-request-details.component';
import {ComponentFixture, TestBed} from '@angular/core/testing';


describe('OperationRequestDetailsComponent', () => {
  let component: OperationRequestDetailsComponent;
  let fixture: ComponentFixture<OperationRequestDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OperationRequestDetailsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(OperationRequestDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
