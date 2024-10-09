import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { from, Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
   }
   getAllUsers(): Observable<any> {
    return from(this.http.get(environment.apiUrl + '/User/get_all_users'));
  }
  createUser(user:any):Observable<any>{
    return from(this.http.post(environment.apiUrl+'/User/create_user',user));
  }
}
