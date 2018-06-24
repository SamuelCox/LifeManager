import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Environment } from './Environment';
import { Observable } from 'rxjs';
import { Login } from './models/login';

@Injectable()
export class LoginService {

  private LOGIN_URL : string = Environment.BASE_URL +  'api/auth/authenticate';
  private REGISTER_URL : string = Environment.BASE_URL +  'api/auth/register';
  
  public LoggedIn : boolean;


  constructor(private http: HttpClient) { }


  login(login : Login) : Observable<Object>
  {
    return this.http.post(this.LOGIN_URL, login);
  }

  register(login : Login) : Observable<Object> {
    return this.http.post(this.REGISTER_URL, login);
  }


}
