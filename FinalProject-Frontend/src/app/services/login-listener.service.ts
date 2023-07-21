import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoginListenerService {
  [x: string]: any;
  private loggedUserStatusSource = new BehaviorSubject<string>("false");
  public loggedUserStatus$ = this.loggedUserStatusSource.asObservable();
  constructor(private authService:AuthService) { 
    var isAuthenticated = authService.isAuthenticated() == true ? "true" : "false";
    this.setLoggedUserStatus(isAuthenticated).subscribe();
  }
  setLoggedUserStatus(state:string):Observable<string>{
    //let ts = window.localStorage.getItem("loggedUserStatus");
    let ts = state == "true" ? "true" : "false";
    this.loggedUserStatusSource.next(ts);
    return this.loggedUserStatus$;
  }


}

