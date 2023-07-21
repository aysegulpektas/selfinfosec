import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ChangePasswordModel } from '../models/changePasswordModel';
import { PasswordResetWithCodeModel } from '../models/forgotPassword/passwordResetWithCodeModel';
import { SendResetMailModel } from '../models/forgotPassword/sendResetMailModel';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { UserProfileModel } from '../models/userProfile/userProfileModel';
import { UserProfileEdit } from '../models/userProfileEdit';
import { UserResponseModel } from '../models/userResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient:HttpClient) { }
  getLoggedUserInformation(){
    let apiUrl = environment.url+"User/GetMe";
    return this.httpClient.get<UserResponseModel>(apiUrl);
  }
  updateUserPassword(changePasswordModel:ChangePasswordModel){
    let apiUrl = environment.url+"User/UpdatePasswordUser";
    return this.httpClient.post<ResponseModel>(apiUrl,changePasswordModel);
  }
  updateUserProfile(updateProfileModel:UserProfileEdit){
    let apiUrl = environment.url+"User/UpdateProfile";
    return this.httpClient.post<ResponseModel>(apiUrl,updateProfileModel);
  }
  getProgress(){
    let apiUrl = environment.url+"UserProfile/GetMyProfile";
    return this.httpClient.get<SingleResponseModel<UserProfileModel>>(apiUrl);
  }
  sendResetCode(sendResetMailModel:SendResetMailModel) {
    let apiUrl = environment.url+"User/SendResetCode";
    return this.httpClient.post(apiUrl,sendResetMailModel,{responseType:'text',observe:'body'});
  }
  resetPasswordWithCode(passwordResetWithCodeModel:PasswordResetWithCodeModel){
    let apiUrl = environment.url+"User/ResetPasswordWithCode";
    return this.httpClient.post(apiUrl,passwordResetWithCodeModel,{responseType:'text',observe:'body'});
  }
}
