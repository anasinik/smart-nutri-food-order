import { Component, OnInit } from '@angular/core';
import { Restaurant } from '../model/restaurant-details.model';
import { RestaurantCardComponent } from '../restaurant-card/restaurant-card.component';
import { CommonModule } from '@angular/common';
import { RestaurantsService } from '../restaurants.service';

@Component({
  selector: 'app-restaurants-overview',
  standalone: true,
  imports: [
    CommonModule,
    RestaurantCardComponent
  ],
  templateUrl: './restaurants-overview.component.html',
  styleUrl: './restaurants-overview.component.css'
})
export class RestaurantsOverviewComponent implements OnInit {
  restaurants: Restaurant[] = [];

  constructor(
    private service: RestaurantsService
  ) { }

  ngOnInit(): void {
    this.service.getAll().subscribe({
      next: (data) => {
        this.restaurants = data;
      }
    });
  }
}
