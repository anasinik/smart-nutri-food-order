import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTabsModule } from '@angular/material/tabs';
import { UserService } from '../../user/user.service';
import { RestaurantsOverviewComponent } from "../../restaurants/restaurants-overview/restaurants-overview.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatToolbarModule,
    MatTabsModule,
    RestaurantsOverviewComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  heroImage = 'home.png';
  isLoggedIn: boolean;

  constructor(private userService: UserService) {
    this.isLoggedIn = this.userService.isLoggedIn();
  }
}
