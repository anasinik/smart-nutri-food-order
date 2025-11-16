import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { RestaurantsService } from '../restaurants.service';

@Component({
  selector: 'app-create-restaurant',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './create-restaurant.component.html',
  styleUrl: './create-restaurant.component.css'
})
export class CreateRestaurantComponent {
  restaurantForm: FormGroup;
  selectedFile: File | null = null;
  previewUrl: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: RestaurantsService
  ) {
    this.restaurantForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      description: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      managerUsername: ['', Validators.required]
    });
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];

      const reader = new FileReader();
      reader.onload = e => (this.previewUrl = e.target?.result as string);
      reader.readAsDataURL(this.selectedFile);
    }
  }

  onSubmit() {
    if (this.restaurantForm.invalid) {
      this.restaurantForm.markAllAsTouched();
      return;
    }

    const data = {
      ...this.restaurantForm.value
    };

    this.service.createRestaurant(data).subscribe({
      next: (result: any) => {
        console.log('Restaurant created:', result);

        const restaurantId = result.id;
        console.log('New restaurant ID:', restaurantId);

        if (this.selectedFile) {
          const formData = new FormData();
          formData.append('file', this.selectedFile);

          this.service.uploadRestaurantPhoto(restaurantId, formData).subscribe({
            next: (res) => {
              console.log('Photo uploaded:', res);
              void this.router.navigate(['/restaurants']);
            },
            error: (err) => console.error('Error uploading photo:', err)
          });
        } else {
          void this.router.navigate(['/restaurants']);
        }
      },
      error: (err) => console.error('Error creating restaurant:', err)
    });

  }
}
