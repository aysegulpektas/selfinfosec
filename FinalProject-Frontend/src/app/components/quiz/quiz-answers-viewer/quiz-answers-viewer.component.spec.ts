import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizAnswersViewerComponent } from './quiz-answers-viewer.component';

describe('QuizAnswersViewerComponent', () => {
  let component: QuizAnswersViewerComponent;
  let fixture: ComponentFixture<QuizAnswersViewerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuizAnswersViewerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizAnswersViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
