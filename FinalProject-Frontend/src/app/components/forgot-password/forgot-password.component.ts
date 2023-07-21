import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SendResetMailModel } from 'src/app/models/forgotPassword/sendResetMailModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  sending:boolean = false;
  constructor(private userService:UserService,private toastrService:ToastrService){}
  ngOnInit(){
    
  }
  sendResetCode(){
    let mailAddress = (document.getElementById("userEmail") as HTMLInputElement).value;
    let sendResetCodeModel:SendResetMailModel = {email:mailAddress};
    this.sending = true;
    this.userService.sendResetCode(sendResetCodeModel).subscribe({
      next:(response)=>{
        console.log(response);
        this.toastrService.info(response as string);
        this.sending = false;
      },error:(err)=>{
        console.log(err);
        this.toastrService.error("Bir hata oluştu");
        this.sending = false;
      }
      
    })
  }
}
