import { Component } from '@angular/core';
import { CartItem } from '../model/cart-item.model';
import { CartService } from '../cart.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-cart-overview',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule, MatTableModule],
  templateUrl: './cart-overview.component.html',
  styleUrl: './cart-overview.component.css'
})
export class CartOverviewComponent {

  cartItems: CartItem[] = [];
  totalPrice: number = 0;
  displayedColumns: string[] = ['mealName', 'quantity', 'price', 'total'];


  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.cartService.getCart().subscribe({
      next: (items: CartItem[]) => {
        this.cartItems = items;
        this.calculateTotal();
      },
      error: (err) => {
        console.error('Failed to load cart', err);
        this.cartItems = [];
        this.totalPrice = 0;
      }
    });
  }


  calculateTotal(): void {
    this.totalPrice = this.cartItems.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );
  }

  placeOrder(): void {
    console.log('Placing order...');
  }
}
