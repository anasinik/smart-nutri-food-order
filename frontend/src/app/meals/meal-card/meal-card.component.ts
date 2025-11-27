import { Component, Input } from '@angular/core';
import { Meal } from '../model/meal-details.model';
import { MealService } from '../meal.service';
import { CommonModule } from '@angular/common';
import { CartService } from '../../cart/cart.service';
import { ToastService } from '../../shared/toast/toast.service';

@Component({
  selector: 'app-meal-card',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './meal-card.component.html',
  styleUrl: './meal-card.component.css'
})
export class MealCardComponent {
  @Input() meal?: Meal;

  constructor(
    private service: MealService,
    private cartService: CartService,
    private toast: ToastService
  ) { }

  getPhotoUrl(mealId: string): string {
    return this.service.getPhotoUrl(mealId);
  }
  
  addToCart() {
    this.cartService.addToCart(this.meal!.id, 1).subscribe({
      next: () => {
        this.toast.show('Meal added to cart!', 'success');
      },
      error: () => {
        this.toast.show('Failed to add meal to cart.', 'error');
      }
    });
  }
}
