import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  constructor(private httpClient:HttpClient) { }
  addImage(formData:FormData){
    let apiUrl = environment.url + "SequencedImages/upload"; 
    return this.httpClient.post(apiUrl,formData,{reportProgress:true,observe:"events"})
  }
}
