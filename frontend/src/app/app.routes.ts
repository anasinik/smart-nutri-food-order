import { Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { RegistrationFormComponent } from './user/registration-form/registration-form.component';
import { LoginFormComponent } from './user/login-form/login-form.component';
import { AuthGuard } from './guard/auth.guard';
import { CreateRestaurantComponent } from './restaurants/create-restaurant/create-restaurant.component';
import { CreateMealComponent } from './meals/create-meal/create-meal.component';
import { MealsOverviewComponent } from './meals/meals-overview/meals-overview.component';
import { CartOverviewComponent } from './order/cart-overview/cart-overview.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationFormComponent },
  { path: 'login', component: LoginFormComponent },
  {
    path: 'create-restaurant',
    component: CreateRestaurantComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'create-meal',
    component: CreateMealComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Manager'] }
  },
  {
    path: 'meals-overview/:restaurantId',
    component: MealsOverviewComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin', 'Customer', 'Manager'] }
  },
  {
    path: 'cart-overview',
    component: CartOverviewComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin', 'Customer', 'Manager'] }
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' }
];
