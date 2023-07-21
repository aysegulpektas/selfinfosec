import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserProfileEdit } from 'src/app/models/userProfileEdit';
import { UserResponseModel } from 'src/app/models/userResponse';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { LoginListenerService } from 'src/app/services/login-listener.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit{

  
  isLogged:boolean = false;
userInfoModel:UserResponseModel;
userInfoModelOriginal:UserResponseModel
constructor(private userService:UserService, private toastrService:ToastrService,private localStorageService:LocalStorageService,private loginListener:LoginListenerService,private router:Router) { }
 

ngOnInit(): void {
   this.loggedWatcher();
}

loggedWatcher(){
  this.loginListener.loggedUserStatus$.subscribe(status=>{
    this.isLogged = status == "true" ? true : false 
    if(this.isLogged == true){
      this.getUserInfo();
    }
  })
}
getUserInfo(){
  this.userService.getLoggedUserInformation().subscribe({
    next:(response)=>{
      this.userInfoModel = response;
      this.userInfoModelOriginal = response;

    },
    error:(err)=>{
      this.toastrService.error("Kullanıcı bilgileri alınırken bir sorun oluştu")
    }
  })
}
cancel(){
  this.userInfoModel = null;
  setTimeout(()=>{
    this.userInfoModel = this.userInfoModelOriginal;
  },500)

}
updateProfile(){
  let firstName = (document.getElementById("firstName") as HTMLInputElement).value;
  let lastName = (document.getElementById("lastName") as HTMLInputElement).value;
  let email = (document.getElementById("email") as HTMLInputElement).value;
  let userProfileUpdate:UserProfileEdit = {firstName:firstName,lastName:lastName,email:email};
  if(firstName != "" && lastName != "" && email != ""){
    this.userService.updateUserProfile(userProfileUpdate).subscribe({
      next:(response)=>{
        if(response.success){
          this.toastrService.success("Profil başarıyla güncellendi");
        }else{
          this.toastrService.error("Profil güncellenirken bir hata oluştu");
        }
      },error:(err)=>{
        this.toastrService.error("Bir hata oluştu");
      }
    })
  }else{
    this.toastrService.error("Lütfen bilgilerinizi kontrol edin");
  }
}

}
