import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/users';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http:HttpClient) { }
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User>(1);//to emit the last value
  $currentUser = this.currentUserSource.asObservable();
  login(model:User){
   return this.http.post(this.baseUrl + 'account/login',model).pipe(
   map((response:any) =>{
    const user = response;
    if(user){
      localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    }
   })
   )
  }
  setCurrentUser(user:User){
   this.currentUserSource.next(user);
  }
logout(){
  localStorage.removeItem('user');
  this.currentUserSource.next(null);
}
register(model:any){
  return this.http.post(this.baseUrl + 'account/register',model).pipe(
    map((user:User)=>{
      if(user){
        localStorage.setItem('user',JSON.stringify(user));
        this.currentUserSource.next(user);
      }
     
    })
  )
}
}
