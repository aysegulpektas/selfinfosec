import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddAnswerModel } from '../models/questions/addAnswerModel';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class AnswerService {

  constructor(private httpClient:HttpClient) { }
  addAnswer(addAnswerModel:AddAnswerModel){
    let apiUrl = environment.url + "Answers/AddAnswer";
    return this.httpClient.post<ResponseModel>(apiUrl,addAnswerModel);
  }
}
