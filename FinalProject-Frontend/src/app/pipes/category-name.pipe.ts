import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'categoryName'
})
export class CategoryNamePipe implements PipeTransform {
  constructor(private translateService:TranslateService){}
  transform(value: string): string {
    let tr:string;
    let en:string;
    let valSplit = value.split(';');
    if(valSplit.length > 0){
      tr = valSplit[0];
      en = valSplit[1];
    }
    let useLang = this.translateService.currentLang || this.translateService.defaultLang;
    return useLang == 'en' ? en:tr;
  }

}
