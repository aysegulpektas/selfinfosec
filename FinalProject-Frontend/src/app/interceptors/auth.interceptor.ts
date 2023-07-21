import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let token = localStorage.getItem("token");
    if(token != null){
      let newRequest:HttpRequest<any>;
      let userLang = localStorage.getItem('language') ?? "tr";
      newRequest = request.clone({
        headers: request.headers.set("Authorization", "Bearer " + token).append('user-language',userLang)
      });
      return next.handle(newRequest);
    }
    return next.handle(request);
  }
}
