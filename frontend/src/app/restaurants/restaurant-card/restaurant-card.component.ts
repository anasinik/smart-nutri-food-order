import { Component, Input } from '@angular/core';
import { Restaurant } from '../model/restaurant-details.model';
import { CommonModule } from '@angular/common';
import { RestaurantsService } from '../restaurants.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-restaurant-card',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './restaurant-card.component.html',
  styleUrls: ['./restaurant-card.component.css'] 
})
export class RestaurantCardComponent {
  @Input() restaurant?: Restaurant;

  constructor(
    private service: RestaurantsService,
    private router: Router
  ) { }

  goToMenu() {
    console.log("Navigating to meals overview for restaurant ID:", this.restaurant?.id);
    this.router.navigate(['meals-overview', this.restaurant?.id]);
  }


  getPhotoUrl(photoPath?: string) {
    return this.service.getPhotoUrl(photoPath);
  }
}
