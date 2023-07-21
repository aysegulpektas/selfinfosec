import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'BEUFinalProject';
  constructor(public translateService:TranslateService){
    this.translateService.addLangs(["tr","en"]);
    this.changeIfSelectedLanguage();
  }
  public onChange(selectedLang:string){

    this.translateService.use(selectedLang);
  }
  changeIfSelectedLanguage(){
    var selectedLang = localStorage.getItem("language");
    if(selectedLang && this.translateService.getLangs().indexOf(selectedLang) != -1){
      this.translateService.use(selectedLang);
    }
  }
}
