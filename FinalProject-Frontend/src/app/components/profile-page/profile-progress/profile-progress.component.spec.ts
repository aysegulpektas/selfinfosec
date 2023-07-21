import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileProgressComponent } from './profile-progress.component';

describe('ProfileProgressComponent', () => {
  let component: ProfileProgressComponent;
  let fixture: ComponentFixture<ProfileProgressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileProgressComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
