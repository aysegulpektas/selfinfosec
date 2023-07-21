import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DarkLightSwitchListenerService } from 'src/app/services/dark-light-switch-listener.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

import { LoginListenerService } from 'src/app/services/login-listener.service';
@Component({
  selector: 'app-navi',
  templateUrl: './navi.component.html',
  styleUrls: ['./navi.component.css']
})
export class NaviComponent implements OnInit {

isLogged:boolean = false;
username:string;
constructor(private darkModeListener:DarkLightSwitchListenerService, private toastrService:ToastrService,private localStorageService:LocalStorageService,private loginListener:LoginListenerService,private router:Router) { }
 

ngOnInit(): void {
   this.loggedWatcher();
   this.automaticDarkMode();
}

loggedWatcher(){
  this.loginListener.loggedUserStatus$.subscribe(status=>{
    this.isLogged = status == "true" ? true : false 
    if(this.isLogged == true){
      this.username = this.localStorageService.storageGetValue("username");
    }
  })
}
logout(){
  this.loginListener.setLoggedUserStatus('false').subscribe();
  localStorage.removeItem("token");
  localStorage.removeItem("expiration");
  localStorage.removeItem("username");
 console.log("çıkış yapıldı")
  this.router.navigate(['/login']);
}
toggleLightDark(){
  let toggleCheck = document.getElementById("darkLight") as HTMLInputElement
  if(toggleCheck.checked){
    document.getElementsByTagName("body")[0].classList.remove("dark");
    document.getElementsByTagName("body")[0].classList.add("dark");
    this.localStorageService.storageSetValue("darkMode","true");
    this.darkModeListener.setDarkMode("true");
  }else{
    document.getElementsByTagName("body")[0].classList.remove("dark");
    this.localStorageService.storageRemoveValue("darkMode");
    this.darkModeListener.setDarkMode("false");
  }
}
automaticDarkMode(){
  if(this.localStorageService.storageIsSet("darkMode")){
    var val = this.localStorageService.storageGetValue("darkMode");
    if(val == "true"){
      let toggleCheck = document.getElementById("darkLight") as HTMLInputElement
      toggleCheck.checked = true;
      document.getElementsByTagName("body")[0].classList.remove("dark");
      document.getElementsByTagName("body")[0].classList.add("dark");
      this.localStorageService.storageSetValue("darkMode","true");
    }
  }
}
}
