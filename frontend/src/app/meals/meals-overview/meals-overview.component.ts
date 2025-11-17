import { Component, OnInit } from '@angular/core';
import { MealCardComponent } from '../meal-card/meal-card.component';
import { Meal } from '../model/meal-details.model';
import { MealService } from '../meal.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

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
    private service: MealService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
      const restaurantId = this.route.snapshot.paramMap.get('restaurantId');

    if (restaurantId)
      this.service.getMealsForRestaurant(restaurantId).subscribe({
        next: (data) => {
          this.meals = data;
        }
      });
    else

      this.service.getAll().subscribe({
        next: (data) => {
          this.meals = data;
        }
      });
  }

}
