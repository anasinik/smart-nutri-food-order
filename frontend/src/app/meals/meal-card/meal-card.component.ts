import { Component, Input } from '@angular/core';
import { Meal } from '../model/meal-details.model';
import { MealService } from '../meal.service';
import { CommonModule } from '@angular/common';

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
    private service: MealService
  ) { }

  getPhotoUrl(mealId: string): string {
    return this.service.getPhotoUrl(mealId);
  }
  
  addToCart() {
    // console.log(`Add meal ${this.meal?.name} to cart`);
  }
}
