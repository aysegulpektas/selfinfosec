import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ChangePasswordModel } from 'src/app/models/changePasswordModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  loading:boolean = false;
  constructor(private translateService:TranslateService, private userService:UserService,private toastrService:ToastrService){

  }
  changePassword(){
    if(this.loading){
      return;
    }
    let oldPasswordInput = document.getElementById("oldPasswordInput") as HTMLInputElement;
    let newPasswordInput = document.getElementById("newPasswordInput") as HTMLInputElement;
    let newPasswordRetryInput = document.getElementById("newPasswordRetryInput") as HTMLInputElement;
    if(newPasswordInput.value != newPasswordRetryInput.value){
      this.toastrService.error(this.translateService.instant('newPasswordAndNewPasswordRetry'));
      return;
    }
    let changePasswordModel:ChangePasswordModel = {oldPassword:oldPasswordInput.value,newPassword:newPasswordInput.value}
    this.loading = true;
    this.userService.updateUserPassword(changePasswordModel).subscribe({
      next:(response)=>{
        this.loading = false;
        if(response.success){
          this.toastrService.success(response.message);
          oldPasswordInput.value = "";
          newPasswordInput.value = "";
          newPasswordRetryInput.value = "";
        }else{
          response.message != '' ? this.toastrService.error(response.message) : this.toastrService.error(this.translateService.instant("errorToastMessage"))
        }
      },error:(err)=>{
        this.loading = false;
        this.toastrService.error(err.error.message)
      }
    })
  }
  cancel(){
    let oldPasswordInput = document.getElementById("oldPasswordInput") as HTMLInputElement;
    let newPasswordInput = document.getElementById("newPasswordInput") as HTMLInputElement;
    let newPasswordRetryInput = document.getElementById("newPasswordRetryInput") as HTMLInputElement;
    oldPasswordInput.value = "";
    newPasswordInput.value = "";
    newPasswordRetryInput.value = "";
  }
}
