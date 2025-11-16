import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { env } from '../../env';
import { Restaurant } from './model/restaurant-details.model';

@Injectable({
  providedIn: 'root'
})
export class RestaurantsService {

  constructor(
    private http: HttpClient
  ) { }

  createRestaurant(data: any): Observable<string> {
    return this.http.post<any>(env + "/api/Restaurant", data);
  }

  uploadRestaurantPhoto(restaurantId: string, file: FormData): Observable<any> {
    return this.http.post<any>(`${env}/api/Restaurant/${restaurantId}/photo`, file);
  }

  getAll(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(env + "/api/Restaurant");
  }

  getPhotoUrl(photoPath?: string): string {
    if (!photoPath) return '';
    return photoPath.startsWith('/images/restaurants/')
      ? `${env}${photoPath}`
      : `${env}/images/restaurants/${photoPath}`;
  }
}
