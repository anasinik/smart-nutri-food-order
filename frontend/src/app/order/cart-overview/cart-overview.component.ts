import { Component } from '@angular/core';
import { CartItem } from '../model/cart-item.model';
import { OrderService } from '../order.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { OrderCheckoutDialogComponent } from '../order-checkout-dialog/order-checkout-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Order } from '../model/order.model';

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


  constructor(private service: OrderService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.service.getCart().subscribe({
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

  openCheckoutDialog() {
    const dialogRef = this.dialog.open(OrderCheckoutDialogComponent, {
      width: '400px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.createOrder(result);
    });
  }

  createOrder(orderData: any) {
    const payload : Order = {
      address: orderData.address,
      paymentMethod: orderData.paymentMethod
    };

    console.log('Creating order with data:', payload);

    this.service.createOrder(payload).subscribe({
      next: (response) => {
        console.log('Order created!', response);
        this.cartItems = [];
        this.totalPrice = 0;
      },
      error: (err) => {
        console.error('Failed to create order', err);
      }
    });
  }

}
