import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingsNaviComponent } from './settings-navi.component';

describe('SettingsNaviComponent', () => {
  let component: SettingsNaviComponent;
  let fixture: ComponentFixture<SettingsNaviComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingsNaviComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SettingsNaviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
