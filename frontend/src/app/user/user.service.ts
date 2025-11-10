import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { env } from '../../env';
import { LoginResponse } from './model/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) { }

  register(data: any): Observable<string> {
    return this.http.post<string>(env + "/api/Auth/registration", data)
  }

  login(data: any): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(env + "/api/Auth/login", data)
  }
}
