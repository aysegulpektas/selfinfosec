import { Injectable } from '@angular/core';
import { LoginModel } from '../models/loginModel';
import{HttpClient} from '@angular/common/http'

import { SingleResponseModel } from '../models/singleResponseModel';
import { RegisterModel } from '../models/registerModel';
import { ResponseModel } from '../models/responseModel';
import { Token } from '@angular/compiler';
import { environment } from 'src/environments/environment';
import { TokenModel } from '../models/tokenModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl = environment.url+"auth/";
  constructor(private httpClient:HttpClient) { }

  login(loginModel:LoginModel){
    return this.httpClient.post<SingleResponseModel<TokenModel>>(this.apiUrl+"login",loginModel)
  }

  register(registerModel:RegisterModel){
    return this.httpClient.post<ResponseModel>(this.apiUrl+"register",registerModel)
  }

getLoggedUser(){
  return this.httpClient.get<SingleResponseModel<RegisterModel>>(this.apiUrl+"getuserbysession");
}

isLogged(){
  return this.httpClient.get<ResponseModel>(this.apiUrl+"islogged");
}
  isAuthenticated(){
    if(localStorage.getItem("token")){//giriş yapan kullanıcıyı localstoragede tutacapız
      let expiration = localStorage.getItem("expiration");
      let expirationDate = new Date(expiration);
      let currentDate = new Date();
      console.log(expirationDate);
      console.log(currentDate);
      console.log(expirationDate.getTime() - currentDate.getTime());
      if(expirationDate < currentDate){
        return false;
      }
      return true;
    }
    else{
      return false;
    }
  }
}
