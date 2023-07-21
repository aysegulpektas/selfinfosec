import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileNaviComponent } from './profile-navi.component';

describe('ProfileNaviComponent', () => {
  let component: ProfileNaviComponent;
  let fixture: ComponentFixture<ProfileNaviComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileNaviComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileNaviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
