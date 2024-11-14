import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddOperationRequestComponent } from './add-operation-request.component';

describe('AddOperationRequestComponent', ()=>{
  let component:AddOperationRequestComponent;
  let fixture: ComponentFixture<AddOperationRequestComponent>;

  beforeEach(async()=>{
    await TestBed.configureTestingModule({
        imports: [AddOperationRequestComponent]
    })
      .compileComponents();

      fixture=TestBed.createComponent(AddOperationRequestComponent);
      component= fixture.componentInstance;
      fixture.detectChanges();
  });

  it('should create', ()=>{
      expect(component).toBeTruthy();
  })

})
