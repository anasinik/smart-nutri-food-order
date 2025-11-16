import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private userService: UserService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('jwt_token');

    if (!token) {
      this.router.navigate(['/login']);
      return false;
    }

    const userRoles = this.userService.getRoles();
    const allowedRoles = route.data['roles'] as Array<string>;

    const hasAccess = userRoles.some(r => allowedRoles.includes(r));

    if (!hasAccess) {
      console.log('Access denied - Users with role', userRoles, 'cannot access this route');
      this.router.navigate(['/home']);
      return false;
    }

    return true;

  }
}
