import { Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { RegistrationFormComponent } from './registration/registration-form/registration-form.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationFormComponent },
  // { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' }
];
