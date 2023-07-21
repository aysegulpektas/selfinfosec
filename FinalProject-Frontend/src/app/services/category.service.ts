import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddCategoryModel } from '../models/category/addCategoryModel';
import { CategoryModel } from '../models/category/categoryModel';
import { ListResponseModel } from '../models/listResponseModel';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  apiUrl:string = environment.url + "categories/";
  constructor(private httpClient:HttpClient) { }
  addCategory(addCategoryModel:AddCategoryModel){
    return this.httpClient.post<ResponseModel>(this.apiUrl+"addCategory",addCategoryModel);
  }
  deleteCategory(categoryId:number){
    return this.httpClient.post<ResponseModel>(this.apiUrl+"deleteCategory?categoryId="+categoryId,null);
  }
  getByCategoryId(categoryId:number){
    return this.httpClient.get<SingleResponseModel<CategoryModel>>(this.apiUrl+"getByCategoryId?categoryId="+categoryId);
  }
  getCategories(){
    return this.httpClient.get<ListResponseModel<CategoryModel>>(this.apiUrl+"getcategories");
  }
  
}
