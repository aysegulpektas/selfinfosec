import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlesHelperComponent } from './articles-helper.component';

describe('ArticlesHelperComponent', () => {
  let component: ArticlesHelperComponent;
  let fixture: ComponentFixture<ArticlesHelperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticlesHelperComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArticlesHelperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
