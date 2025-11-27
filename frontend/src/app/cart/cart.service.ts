import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CartItem } from './model/cart-item.model';
import { HttpClient } from '@angular/common/http';
import { env } from '../../env';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(
    private http: HttpClient
  ) { }

  getCart(): Observable<CartItem[]> {
    return this.http.get<{ items: CartItem[] }>(`${env}/api/Cart`)
      .pipe(
        map(response => response.items)
      );
  }

  addToCart(mealId: string, quantity: number): Observable<void> {
    return this.http.post<void>(`${env}/api/Cart/items`, { mealId, quantity });
  }
}
