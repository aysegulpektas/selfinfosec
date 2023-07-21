import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserResponseModel } from 'src/app/models/userResponse';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { LoginListenerService } from 'src/app/services/login-listener.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-settings-navi',
  templateUrl: './settings-navi.component.html',
  styleUrls: ['./settings-navi.component.css']
})

export class SettingsNaviComponent implements OnInit{
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
  preparingPage(){
    this.toastrService.info("Bu sayfa henüz hazır değil. Lütfen daha sonra tekrar deneyin :(")
  }
}

