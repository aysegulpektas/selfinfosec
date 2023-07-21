
import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl, Validators, FormBuilder  } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import { RegisterModel } from 'src/app/models/registerModel';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { AdminService } from 'src/app/services/admin.service';
import { TranslateService } from '@ngx-translate/core';
import { DynamicFormValidatorService } from 'src/app/services/dynamic-form-validator.service';
import { Router } from '@angular/router';
import { LoginListenerService } from 'src/app/services/login-listener.service';
//import{HttpClient} from '@angular/common/http'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  islogged:boolean = false;
  currentUser:RegisterModel;
  isLoggedLoaded:boolean = false;
  sending:boolean = false;
  constructor(private formBuilder:FormBuilder, private authService:AuthService, private toastrService:ToastrService,private localStorageService:LocalStorageService,private translateService:TranslateService,private dynamicFormValidator:DynamicFormValidatorService,private router:Router,private loginListener:LoginListenerService){}
loginForm: FormGroup ;

  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm(){
    let storagedUsername = localStorage.getItem("storagedUsername") ?? "";
    console.log(storagedUsername);
    this.loginForm= this.formBuilder.group({
      username:[storagedUsername,[Validators.required,Validators.pattern("^[a-zA-Z0-9_.]+$")]],
      password:["", Validators.required]
    })
  }
  login(){
    let isFormValid = this.dynamicFormValidator.validate(this.loginForm);
    if(this.sending == true){
      return;
    }
    
    if(isFormValid){
      console.log(this.loginForm.value);
      this.sending = true;
      let loginModel=Object.assign({},this.loginForm.value)

      this.authService.login(loginModel).subscribe({
      
        next:(response)=>{
          this.sending = false;
          if(response.success){
            this.rememberMeControl();
            console.log(response);
            this.localStorageService.storageSetValue("token",response.data.token);
            this.localStorageService.storageSetValue("username",response.data.username);
            this.localStorageService.storageSetValue("expiration",response.data.expiration);
            this.localStorageService.storageSetValue("role",response.data.userRole);
            this.loginListener.setLoggedUserStatus("true").subscribe();
            this.toastrService.success(response.message)
            this.router.navigate(["/articles"]);
          }else{
            this.toastrService.error(response.message);
          }

        },error:(err)=>{
          this.sending = false;
          console.log(err);
          err.error.message != null ? this.toastrService.error(err.error.message) : this.toastrService.error("Bilinmeyen Hata");
        }

        //localStorage.setItem("token", response.data.token)
        
        }
          )
    }
  }






  isLogged(){
    return this.authService.isLogged().subscribe(response=>{
      this.isLoggedLoaded = true;
      this.islogged = response.success;
      this.getLoggedUser();
    })
  }


  getLoggedUser(){
    if(this.islogged == true){
      this.authService.getLoggedUser().subscribe(response=>{
        if(response.success){
          this.currentUser = response.data;
          console.log(this.currentUser);
        }
      },responseError=>{
        console.log(responseError);
        this.toastrService.error(responseError.error.message);
      })
    }
}
rememberMeControl(){
  let input = document.getElementById("usernameInput") as HTMLInputElement;
  let rememberCheck = document.getElementById("rememberMeCheck") as HTMLInputElement;
  if(rememberCheck.checked){
    this.localStorageService.storageSetValue("storagedUsername",input.value);
  }
}
}