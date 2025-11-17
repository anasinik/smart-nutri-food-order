import { Component, OnInit } from '@angular/core';
import { MealCardComponent } from '../meal-card/meal-card.component';
import { Meal } from '../model/meal-details.model';
import { MealService } from '../meal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-meals-overview',
  standalone: true,
  imports: [
    MealCardComponent,
    CommonModule
  ],
  templateUrl: './meals-overview.component.html',
  styleUrl: './meals-overview.component.css'
})
export class MealsOverviewComponent implements OnInit {
  meals: Meal[] = [];

  constructor(
    private service: MealService
  ) { }

  ngOnInit(): void {
    this.service.getAll().subscribe({
      next: (data) => {
        this.meals = data;
      }
    });
  }

}
