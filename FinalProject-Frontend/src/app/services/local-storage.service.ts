import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }
  storageSetValue(key:string,value:string){
    localStorage.setItem(key,value);
  }
  storageGetValue(key:string){
    return localStorage.getItem(key);
  }
  storageRemoveValue(key:string){
    localStorage.removeItem(key);
  }
  storageIsSet(key:string){
    if(localStorage.getItem(key) != null){
        return true;
    } 
    return false;
  }
}
