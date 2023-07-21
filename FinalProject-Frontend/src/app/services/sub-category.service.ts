import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ListResponseModel } from '../models/listResponseModel';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { AddSubCategoryModel } from '../models/subcategory/addSubCategoryModel';
import { SubCategoryModel } from '../models/subcategory/subCategoryModel';

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService {
  apiUrl:string = environment.url+"subcategory/";
  constructor(private httpClient:HttpClient) { }
  addSubCategory(addSubCategoryModel:AddSubCategoryModel){
    return this.httpClient.post<ResponseModel>(this.apiUrl+"addSubcategory",addSubCategoryModel);
  }
  deleteSubCategory(subCategoryId:number){
    return this.httpClient.post<ResponseModel>(this.apiUrl+"deleteSubCategory?subcategoryId="+subCategoryId,null)
  }
  getSubCategories(){
    return this.httpClient.get<ListResponseModel<SubCategoryModel>>(this.apiUrl+"getSubcategories");
  }
  getById(subcategoryId:number){
    return this.httpClient.get<SingleResponseModel<SubCategoryModel>>(this.apiUrl+"getBySubcategoryId?subcategoryId="+subcategoryId);
  }
  getByCategoryId(categoryId:number){
    return this.httpClient.get<ListResponseModel<SubCategoryModel>>(this.apiUrl+"getAllByCategoryId?categoryId="+categoryId);
  }
}
