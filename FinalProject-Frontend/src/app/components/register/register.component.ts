import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

import { FormBuilder, FormGroup, Validators, FormControl, MinLengthValidator } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DynamicFormValidatorService } from 'src/app/services/dynamic-form-validator.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  sending:boolean = false;
  constructor(private formBuilder: FormBuilder, private authService: AuthService, private toastrService: ToastrService,private dynamicFormValidator:DynamicFormValidatorService) { }
  registerForm: FormGroup;

  ngOnInit(): void {
    this.createRegisterModel();
  }

  createRegisterModel() {
    this.registerForm = this.formBuilder.group({
      username: ["", [Validators.required,Validators.minLength(8),Validators.pattern("^[a-zA-Z0-9_.]+$")]],
      firstName: ["", [Validators.required,Validators.pattern("^[A-Za-zişüğçöİŞÜĞÇÖı ]+$")]],
      lastName: ["", [Validators.required,Validators.pattern("^[A-Za-zişüğçöİŞÜĞÇÖı ]+$")]],
      email: ["", Validators.required],
      password: ["", [Validators.required,Validators.minLength(8)]]
    })

  }

  register() {
    if(this.sending){
      return;
    }
    let validateResult = this.dynamicFormValidator.validate(this.registerForm);
    if (validateResult) {
      console.log(this.registerForm.value)
      let registerModel = Object.assign({}, this.registerForm.value)
      this.sending = true;
      this.authService.register(registerModel).subscribe({
        next:(response)=>{
          this.sending = false;
          if(response.success){
            this.toastrService.success(response.message);
            this.registerForm.reset();
          }else{
            this.toastrService.error(response.message);
          }
        },
        error:(err)=>{
          this.sending = false;
          this.toastrService.error("Bir hata oluştu");
        }
      })
    }
  }

}
