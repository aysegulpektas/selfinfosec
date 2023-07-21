import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddQuestionGroupModel } from '../models/questions/addQuestionGroupModel';
import { QuestionGroupModel } from '../models/questions/questionGroupModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class QuestionGroupService {

  constructor(private httpClient:HttpClient) { }
  addQuestionGroup(questionGroupModel:AddQuestionGroupModel):Observable<SingleResponseModel<QuestionGroupModel>>{
    let apiUrl = environment.url+"Question/addquestiongroup";
    return this.httpClient.post<SingleResponseModel<QuestionGroupModel>>(apiUrl,questionGroupModel)
  }
}
