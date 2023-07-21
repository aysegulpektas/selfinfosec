import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ArticleDataModel } from '../models/articleDataModel';
import { ArticleFilterDto } from '../models/articleFilterDto';
import { ArticleInfoModel } from '../models/articleInfoModel';
import { ListResponseModel } from '../models/listResponseModel';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private httpClient:HttpClient) { }
  getAllArticles(filter?:ArticleFilterDto){
    let subcategories = "";
    let title = "";
    if(filter != null){
      if(filter.subcategories != null && filter.subcategories.length > 0){
        for(let subcategory of filter.subcategories){
          if(subcategories == ""){
            subcategories += subcategory;
          }else{
            subcategories+=","+subcategory;
          }
        }
      }
      if(filter.title != null){
        title = filter.title;
      }
    }
    let param = "?";
    if(subcategories != ""){
      param+="subcategories="+subcategories;
    }
    if(title != ""){
      param == "" ? param+="title="+title : param+="&"+"title="+title;
    }
    let apiUrl = environment.url+"articles/getallarticles"+param;
    return this.httpClient.get<ListResponseModel<ArticleInfoModel>>(apiUrl);
  }
  getArticleContent(articleId:number){
   let apiUrl =  environment.url+"articles/getarticle?articleId="+articleId;
   return this.httpClient.get<SingleResponseModel<ArticleDataModel>>(apiUrl);
  }
  addArticle(article:any){
    let apiUrl = environment.url +"articles/addarticle";
    return this.httpClient.post<ResponseModel>(apiUrl,article);
  }
  deleteArticle(articleId:number):Observable<ResponseModel>{
    let apiUrl = environment.url+"Articles/DeleteArticle?articleId="+articleId;
    return this.httpClient.delete<ResponseModel>(apiUrl);
  }
}
