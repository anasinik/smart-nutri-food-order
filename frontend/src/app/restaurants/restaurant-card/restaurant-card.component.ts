import { Component, Input } from '@angular/core';
import { Restaurant } from '../model/restaurant-details.model';
import { CommonModule } from '@angular/common';
import { RestaurantsService } from '../restaurants.service';

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
    private service: RestaurantsService
  ) { }

  goToMenu() {
    // console.log(`Go to menu for ${this.restaurant.name}`);
  }

  getPhotoUrl(photoPath?: string) {
    return this.service.getPhotoUrl(photoPath);
  }
}
