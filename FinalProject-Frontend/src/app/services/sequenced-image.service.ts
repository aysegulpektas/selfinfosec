import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ListResponseModel } from '../models/listResponseModel';
import { SequencedImageModel } from '../models/sequencedImageModel';

@Injectable({
  providedIn: 'root'
})
export class SequencedImageService {

  constructor(private httpClient:HttpClient) { }
  getImages(articleId:number):Observable<ListResponseModel<SequencedImageModel>>{
    var apiUrl = environment.url;
    return this.httpClient.get<ListResponseModel<SequencedImageModel>>(apiUrl+"SequencedImages/getimages?article="+articleId);
  }

}
