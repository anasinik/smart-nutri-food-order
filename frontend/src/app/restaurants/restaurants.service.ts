import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { env } from '../../env';

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
}
