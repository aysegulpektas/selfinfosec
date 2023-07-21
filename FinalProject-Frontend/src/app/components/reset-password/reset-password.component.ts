import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { PasswordResetWithCodeModel } from 'src/app/models/forgotPassword/passwordResetWithCodeModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  loading:boolean = false;
  constructor(private userService:UserService,private toastrService:ToastrService,private translateService:TranslateService){

  }
  ngOnInit(){

  }
  resetPassword(){
    let emailInput = (document.getElementById("userEmail") as HTMLInputElement).value;
    let resetCode = (document.getElementById("resetCode") as HTMLInputElement).value;
    let newPassword = (document.getElementById("newPassword") as HTMLInputElement).value;
    if(emailInput.trim() == '' || resetCode.trim() == '' || newPassword == ''){
      this.toastrService.error(this.translateService.instant('errorToastMessage'));
      return;
    }
    this.loading = true;
    let passwordResetModel:PasswordResetWithCodeModel = {email:emailInput,code:resetCode,newPassword:newPassword};
    this.userService.resetPasswordWithCode(passwordResetModel).subscribe({
      next:(response)=>{
        this.toastrService.info(response);
        this.loading = false;
      },
      error:(err)=>{
        this.toastrService.error(this.translateService.instant('errorToastMessage'));
        this.loading = false;
      }
    })
  }
}
