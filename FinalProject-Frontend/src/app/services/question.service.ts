import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ListResponseModel } from '../models/listResponseModel';
import { AddedQuestionModel } from '../models/questions/addedQuestionModel';
import { AddQuestionModel } from '../models/questions/addQuestionModel';
import { QuestionGroupModel } from '../models/questions/questionGroupModel';
import { QuestionModel } from '../models/questions/questionModel';
import { QuestionWithAnswerModel } from '../models/questions/questionWithAnswersModel';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  constructor(private httpClient:HttpClient) { }
  addQuestion(questionModel:AddQuestionModel):Observable<SingleResponseModel<AddedQuestionModel>>{
    var apiUrl = environment.url+"Question/AddQuestion";
    return this.httpClient.post<SingleResponseModel<AddedQuestionModel>>(apiUrl,questionModel);
  }
  getQuestionGroupsWithArticleId(articleId:number){
    var apiUrl = environment.url;
    return this.httpClient.get<ListResponseModel<QuestionGroupModel>>(apiUrl+"Question/GetQuestionGroupsByArticleId?articleId="+articleId);
  }
  getQuestionsWithQuestionGroupId(questionGroupId:string){
    var apiUrl = environment.url;
    return this.httpClient.get<ListResponseModel<QuestionModel>>(apiUrl+"Question/GetQuestionsByQuestionGroupId?questionGroupId="+questionGroupId);
  }
  getQuizAnswers(questionGroupId:number){
    var apiUrl = environment.url;
    return this.httpClient.get<ListResponseModel<QuestionWithAnswerModel>>(apiUrl+"Question/GetQuizAnswers?questionGroupId="+questionGroupId);
  }
  answerQuestion(question:number,answer:number){
    var apiUrl = environment.url;
    return this.httpClient.get(apiUrl+`Answers/AnswerQuestion?question=${question}&answer=${answer}`,{observe:'response',responseType:'text'});
  }
  endQuiz(questionGroupId:number){
    var apiUrl = environment.url;
    return this.httpClient.get<ResponseModel>(apiUrl+"Question/endquiz?questionGroupId="+questionGroupId);
  }
  //burada cevaplamak için bir fonksiyon yazacağız
    //şu anda yalnızca soru gösterme işlemini yaptık kullanıcı henüz soruya cevap veremiyor. Onu gerçekleştirecek fonksiyonu buraya yazaacağız
    //bunu githuba atmayacağız üstünde bırakıyorum yorumları. incelersinn.
}
