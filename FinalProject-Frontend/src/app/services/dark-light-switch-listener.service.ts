import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DarkLightSwitchListenerService {
  private isDarkModeListener = new BehaviorSubject<string>("false");
  
  constructor() { 
    let isDarkModeEnabled = localStorage.getItem("darkMode") == "true" ? "true" : "false";
    this.setDarkMode(isDarkModeEnabled).subscribe()
  }
  public isDarkMode$ = this.isDarkModeListener.asObservable();
  public setDarkMode(isDark:string):Observable<string>{
    let dark = isDark == "true" ? "true" : "false";
    this.isDarkModeListener.next(dark);
    return this.isDarkMode$;
  }
}
