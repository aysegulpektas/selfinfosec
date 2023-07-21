import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { QuestionModel } from 'src/app/models/questions/questionModel';
import { QuestionService } from 'src/app/services/question.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent {
  constructor(private questionService:QuestionService,private activatedRoute:ActivatedRoute,private toastrService:ToastrService,private router:Router){}
  questionList:QuestionModel[] = null; //bu liste
  currentQuestion:number = 0; //bu index bunlara göre değiştirirsen olacak.
  quizEnd:boolean = false;
  questionGroupId:number = 0;
  selectedAnswer:number = -1;
  
  ngOnInit(){
    this.activatedRoute.params.subscribe(param=>{
      let id = param['id'];
      this.questionGroupId = id;
      this.getQuestionsWithQuestionId(id);
    })
  }
  getQuestionsWithQuestionId(questionGroupId:string){
    this.questionService.getQuestionsWithQuestionGroupId(questionGroupId).subscribe({
      next:(response)=>{
        if(response.success){
          this.questionList = response.data;
          console.log(this.questionList);
        }else{
          this.toastrService.error(response.message ?? "Sınava erişiminiz yok");
          this.quizEnd = true;
        }

      }
    })
  }
  initializeQuiz(){

  }
  endQuiz(){
    if(this.selectedAnswer > 0){
      this.answerQuestion(this.selectedAnswer).then(success=>{
        if(success == false){
          return;
        }
      })
    }
    this.questionService.endQuiz(this.questionGroupId).subscribe({next:(response=>{
      if(response.success){
        this.toastrService.success("Sınav bitti");
        this.quizEnd = true;
      }else{
        this.toastrService.error(response.message ?? "Bir hata oluştu");
      }
    })});

  }
  nextQuestion(){
    if(this.selectedAnswer > 0){
      this.answerQuestion(this.selectedAnswer).then(success=>{
        this.selectedAnswer = -1;
        if(success){
          if(this.questionList.length > this.currentQuestion+1){
            this.currentQuestion++;
          }else{
            this.toastrService.info("Sınav bitti");
            this.quizEnd = true;
          }
        }else{
          this.toastrService.error("Soru cevaplanamadı");
        }
      })
    }


  }
  selectAnswer(answerId:number) {
    this.selectedAnswer = answerId;
  }
  answerQuestion(answerId:number):Promise<boolean>{
    return new Promise((resolve,reject)=>{
      let questionId = this.questionList[this.currentQuestion].questionId;
      this.questionService.answerQuestion(questionId,answerId).subscribe({
        next:(response)=>{
          console.log(response);
          this.toastrService.info(response.body.toString());
          resolve(true);
        },
        error:(err)=>{
          console.log(err);
          this.toastrService.error(err.error);
          resolve(false);
        }
      })

    })

  }
  prevQuestion(){
    
  }
  showAnswers(){
    this.router.navigate(['/quiz-answers/'+this.questionGroupId]);
  }
}
