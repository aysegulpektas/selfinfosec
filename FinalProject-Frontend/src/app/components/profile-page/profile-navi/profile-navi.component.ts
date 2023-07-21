import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserResponseModel } from 'src/app/models/userResponse';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { LoginListenerService } from 'src/app/services/login-listener.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile-navi',
  templateUrl: './profile-navi.component.html',
  styleUrls: ['./profile-navi.component.css']
})

export class ProfileNaviComponent {
  userInfoModel:UserResponseModel;
  isLogged:boolean = false;
  constructor(private userService:UserService, private toastrService:ToastrService,private localStorageService:LocalStorageService,private loginListener:LoginListenerService,private router:Router){}
  ngOnInit(){
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
      },
      error:(err)=>{
        this.toastrService.error("Kullanıcı bilgileri alınırken bir sorun oluştu")
      }
    })
  }
  
}
