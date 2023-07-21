import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class DynamicFormValidatorService {

  constructor(private toastrService:ToastrService,private translateService:TranslateService) { }
  validate(formGroup:FormGroup,showToast:boolean=true){
    var isValid = formGroup.valid;
    var fieldNames = Object.keys(formGroup.controls);
    this.toastrService.clear();
    fieldNames.forEach(fieldName=>{

      var errorList = formGroup.get(fieldName).errors;
      console.log(errorList);
      if(errorList != undefined){
      var getFieldLabel = this.translateService.instant(fieldName);
      var errorType = Object.keys(errorList)[0];
      var additionalParams:{} = Object.values(errorList)[0];
      var params = {"fieldName":getFieldLabel,...additionalParams}
      let error:string = "";
      if(errorType != "pattern"){
        var translateErrorName = 'formError.'+errorType;
        error = this.translateService.instant(translateErrorName,params);
      }else{
        error = this.regexMessages(params);
      }

      if(showToast){
        if(!error.startsWith("formError") && error != ""){
          this.toastrService.error(error);
        }else{
          console.log(error);
        }

      }
    }
    })
    if(isValid){
      return true;
    }else{
      return false;
    }
  }
  regexMessages(params:any):string{
    let regexPattern = params["requiredPattern"];
    let regexType = "";
    switch(regexPattern){
      case "^[A-Za-zişüğçöİŞÜĞÇÖı ]+$":
        regexType = "onlyLetters";
        break;
      case "^[a-zA-Z0-9_.]+$":
        regexType = "letterNumberUnderScoreAndPoint";
        break;
    }
    return this.translateService.instant("formError.regexPattern."+regexType,params);
  }
}
