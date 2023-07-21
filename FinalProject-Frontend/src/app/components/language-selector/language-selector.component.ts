import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.css']
})
export class LanguageSelectorComponent {
  selectedLang:string;
  constructor(private translateService:TranslateService) {}
  ngOnInit(){
    this.selectedLang = this.translateService.currentLang || this.translateService.defaultLang;
    console.log(this.selectedLang)
  }
  selectLanguage(langCode:string){
    this.translateService.use(langCode);
    localStorage.setItem("language",langCode);
    this.selectedLang = langCode;
  }
}
