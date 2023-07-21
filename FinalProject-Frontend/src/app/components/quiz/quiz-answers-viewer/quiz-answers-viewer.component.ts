import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { QuestionWithAnswerModel } from 'src/app/models/questions/questionWithAnswersModel';
import { QuestionService } from 'src/app/services/question.service';

@Component({
  selector: 'app-quiz-answers-viewer',
  templateUrl: './quiz-answers-viewer.component.html',
  styleUrls: ['./quiz-answers-viewer.component.css']
})
export class QuizAnswersViewerComponent {
  questionGroupId: number = -1;
  quizAnswers: QuestionWithAnswerModel[] = null;
  currentQuestion: number = 0;
  quizEnd: boolean = false;
  constructor(private questionService: QuestionService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute,private router:Router) { }
  ngOnInit() {
    this.activatedRoute.params.subscribe(param => {
      this.questionGroupId = param['id'];
      this.getQuizAnswers();
    })
  }
  getQuizAnswers() {
    this.questionService.getQuizAnswers(this.questionGroupId).subscribe({
      next: (response) => {
        console.log(response);
        if (response.success) {
          this.quizAnswers = response.data;
        } else {
          this.toastrService.error(response.message != "" && response.message != undefined ? response.message : "Cevaplar alınırken bir hata oluştu");
        }
      }, error: (err) => {
        this.toastrService.error("Cevaplar alınırken bir hata oluştu");
      }
    })
  }
  nextQuestion() {
    if (this.quizAnswers.length > this.currentQuestion + 1) {
      this.currentQuestion++;
    } else {
      this.quizEnd = true;
    }
  }
  exitPage(){
    this.router.navigate(['/articles']);
  }
}
