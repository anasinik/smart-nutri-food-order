import { Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { RegistrationFormComponent } from './user/registration-form/registration-form.component';
import { LoginFormComponent } from './user/login-form/login-form.component';
import { RestaurantsOverviewComponent } from './restaurants/restaurants-overview/restaurants-overview.component';
import { AuthGuard } from './guard/auth.guard';
import { CreateRestaurantComponent } from './restaurants/create-restaurant/create-restaurant.component';
import { CreateMealComponent } from './meals/create-meal/create-meal.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationFormComponent },
  { path: 'login', component: LoginFormComponent },
  { 
    path: 'restaurants-overview', 
    component: RestaurantsOverviewComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Customer'] }
  },
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
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' }
];
