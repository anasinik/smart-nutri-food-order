import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { env } from '../../env';
import { Meal } from './model/meal-details.model';

@Injectable({
  providedIn: 'root'
})
export class MealService {

  constructor(
      private http: HttpClient
    ) { }
  
    createMeal(data: any): Observable<string> {
      console.log('JWT token:', localStorage.getItem('jwt_token'));
      return this.http.post<any>(env + "/api/Meal", data);
    }
  
    uploadMealPhoto(mealId: string, file: FormData): Observable<any> {
      return this.http.post<any>(`${env}/api/Meal/${mealId}/photo`, file);
    }
  
    getAll(): Observable<Meal[]> {
      return this.http.get<Meal[]>(env + "/api/Meal");
    }

    getMealsForRestaurant(restaurantId: string): Observable<Meal[]> {
      return this.http.get<Meal[]>(`${env}/api/Meal/${restaurantId}`);
    }
  
    getPhotoUrl(photoPath?: string): string {
      if (!photoPath) return '';
      return photoPath.startsWith('/images/meals/')
        ? `${env}${photoPath}`
        : `${env}/images/meals/${photoPath}`;
    }
  }
  