import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilePictureMenuComponent } from './profile-picture-menu.component';

describe('ProfilePictureMenuComponent', () => {
  let component: ProfilePictureMenuComponent;
  let fixture: ComponentFixture<ProfilePictureMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfilePictureMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfilePictureMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
