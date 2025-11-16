import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { env } from '../../env';
import { LoginResponse } from './model/login-response.model';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  register(data: any): Observable<string> {
    return this.http.post<string>(env + "/api/Auth/registration", data)
  }

  login(data: any): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(env + "/api/Auth/login", data)
  }

  logout() {
    localStorage.clear()
    void this.router.navigate(['/login'])
  }

  getRole(): string | null {
    const token = localStorage.getItem('jwt_token');
    if (!token) return null;

    const decoded: any = jwtDecode(token);
    return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('jwt_token');
  }

}
