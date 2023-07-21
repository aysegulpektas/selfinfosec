import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AddAnswerModel } from 'src/app/models/questions/addAnswerModel';
import { AddedQuestionModel } from 'src/app/models/questions/addedQuestionModel';
import { AddQuestionGroupModel } from 'src/app/models/questions/addQuestionGroupModel';
import { AddQuestionModel } from 'src/app/models/questions/addQuestionModel';
import { QuestionGroupModel } from 'src/app/models/questions/questionGroupModel';
import { AnswerService } from 'src/app/services/answer.service';
import { QuestionGroupService } from 'src/app/services/question-group.service';
import { QuestionService } from 'src/app/services/question.service';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent {
  addedQuestionGroupModel: QuestionGroupModel;
  addedQuestionModel: AddedQuestionModel;
  answer1Added: boolean = false;
  answer2Added: boolean = false;
  answer3Added: boolean = false;
  answer4Added: boolean = false;
  newQuestion:boolean = false;
  loading:boolean = false;
  selectedArticle:number = -1;
  constructor(private answerService: AnswerService, private questionService: QuestionService, private questionGroupService: QuestionGroupService, private toastrService: ToastrService) {

  }

  ngOnInit() {

  }
  add(){

    if(this.newQuestion){
      this.addQuestion();

    }else{
      this.addQuestionGroup();
    }

  }
  addQuestionGroup() {
    let groupTitleEl = document.getElementById("groupTitle") as HTMLInputElement;
    if(this.selectedArticle == -1){
      this.toastrService.error("Lütfen makale seçin");
      return;
    }
    this.loading = true;
    let useForScore = document.getElementById("useForScore") as HTMLInputElement;

    let addQuestionGroupModel: AddQuestionGroupModel = { articleId: this.selectedArticle, groupTitle: groupTitleEl.value, useForScore: useForScore.checked };
    this.questionGroupService.addQuestionGroup(addQuestionGroupModel).subscribe({
      next: (response) => {
        this.addedQuestionGroupModel = response.data;
        this.newQuestion = true;
        this.addQuestion();
      },
      error: (err) => {
        this.toastrService.error("Bölüm eklenemedi");
      }
    })

  }
  addNewQuestion(){
    let questionTextEl = document.getElementById("questionText") as HTMLInputElement;

    let answer1El = document.getElementById("answerText1") as HTMLInputElement;
    let answer1CheckEl = document.getElementById("answerText1_True") as HTMLInputElement;

    let answer2El = document.getElementById("answerText2") as HTMLInputElement;
    let answer2CheckEl = document.getElementById("answerText2_True") as HTMLInputElement;

    let answer3El = document.getElementById("answerText3") as HTMLInputElement;
    let answer3CheckEl = document.getElementById("answerText3_True") as HTMLInputElement;

    let answer4El = document.getElementById("answerText4") as HTMLInputElement;
    let answer4CheckEl = document.getElementById("answerText4_True") as HTMLInputElement;

    questionTextEl.value = "";
    answer1El.value = "";
    answer1CheckEl.checked = false;

    answer2El.value = "";
    answer2CheckEl.checked = false;

    answer3El.value = "";
    answer3CheckEl.checked = false;

    answer4El.value = "";
    answer4CheckEl.checked = false;

    this.answer1Added = false;
    this.answer2Added = false;
    this.answer3Added = false;
    this.answer4Added = false;

  }
  addQuestion() {
    let questionTextEl = document.getElementById("questionText") as HTMLInputElement;
    let addQuestionModel: AddQuestionModel = { questionGroupId: this.addedQuestionGroupModel.questionGroupId, questionText: questionTextEl.value, questionType: 0 }
    this.loading = true;
    this.questionService.addQuestion(addQuestionModel).subscribe({
      next: (response) => {
        this.addedQuestionModel = response.data;
        this.addAnswer();
      },
      error: (err) => {
        this.toastrService.error("Soru eklenemedi");
      }
    })
  }
  addAnswer() {
    let answer1El = document.getElementById("answerText1") as HTMLInputElement;
    let answer1CheckEl = document.getElementById("answerText1_True") as HTMLInputElement;

    let answer2El = document.getElementById("answerText2") as HTMLInputElement;
    let answer2CheckEl = document.getElementById("answerText2_True") as HTMLInputElement;

    let answer3El = document.getElementById("answerText3") as HTMLInputElement;
    let answer3CheckEl = document.getElementById("answerText3_True") as HTMLInputElement;

    let answer4El = document.getElementById("answerText4") as HTMLInputElement;
    let answer4CheckEl = document.getElementById("answerText4_True") as HTMLInputElement;

    let answerModel1: AddAnswerModel = { answerText: answer1El.value, isTrue: answer1CheckEl.checked, questionId: this.addedQuestionModel.questionId };
    let answerModel2: AddAnswerModel = { answerText: answer2El.value, isTrue: answer2CheckEl.checked, questionId: this.addedQuestionModel.questionId };
    let answerModel3: AddAnswerModel = { answerText: answer3El.value, isTrue: answer3CheckEl.checked, questionId: this.addedQuestionModel.questionId };
    let answerModel4: AddAnswerModel = { answerText: answer4El.value, isTrue: answer4CheckEl.checked, questionId: this.addedQuestionModel.questionId };
    if(answerModel1.isTrue  == false && answerModel2.isTrue == false && answerModel3.isTrue ==false && answerModel4.isTrue == false){
      this.toastrService.error("En az 1 doğru cevap olmalıdır");
      return;
    }
    this.answerService.addAnswer(answerModel1).subscribe(
      {
        next: (response) => {
          if (response.success) {
            this.answer1Added = true;
            this.checkLoading();
          }
        }, error: (err) => {
          this.toastrService.error("1. Seçenek eklenemedi");
        }
      }
    )

    this.answerService.addAnswer(answerModel2).subscribe(
      {
        next: (response) => {
          if (response.success) {
            this.answer2Added = true;
            this.checkLoading();
          }
        }, error: (err) => {
          this.toastrService.error("2. Seçenek eklenemedi");
        }
      }
    )

    this.answerService.addAnswer(answerModel3).subscribe(
      {
        next: (response) => {
          if (response.success) {
            this.answer3Added = true;
            this.checkLoading();
          }
        }, error: (err) => {
          this.toastrService.error("3. Seçenek eklenemedi");
        }
      }
    )

    this.answerService.addAnswer(answerModel4).subscribe(
      {
        next: (response) => {
          if (response.success) {
            this.answer4Added = true;
            this.checkLoading();
          }
        }, error: (err) => {
          this.toastrService.error("4. Seçenek eklenemedi");
        }

      }
    )
  }
  checkLoading(){
    if(this.answer1Added && this.answer2Added && this.answer3Added && this.answer4Added){
      this.loading = false;
    }
  }
  getArticleId(selArticleId:number){
    this.selectedArticle = selArticleId;
  }

}
