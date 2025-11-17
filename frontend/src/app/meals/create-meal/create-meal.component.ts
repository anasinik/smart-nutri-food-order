import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { MealService } from '../meal.service';

@Component({
  selector: 'app-create-meal',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './create-meal.component.html',
  styleUrl: './create-meal.component.css'
})
export class CreateMealComponent {
  mealForm: FormGroup;
  selectedFile: File | null = null;
  previewUrl: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: MealService
  ) {
    this.mealForm = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(0.1)]],
    calories: [0, [Validators.required, Validators.min(0)]],
    proteins: [0, [Validators.required, Validators.min(0)]],
    carbohydrates: [0, [Validators.required, Validators.min(0)]],
    sugars: [0, [Validators.required, Validators.min(0)]],
    isVegan: [false],
    photoPath: ['']
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
    if (this.mealForm.invalid) {
      this.mealForm.markAllAsTouched();
      return;
    }

    const data = {
      ...this.mealForm.value
    };

    this.service.createMeal(data).subscribe({
      next: (result: any) => {
        console.log('meal created:', result);

        const mealId = result.id;
        console.log('New meal ID:', mealId);

        if (this.selectedFile) {
          const formData = new FormData();
          formData.append('file', this.selectedFile);

          this.service.uploadMealPhoto(mealId, formData).subscribe({
            next: (res) => {
              console.log('Photo uploaded:', res);
              void this.router.navigate(['/home']);
            },
            error: (err) => console.error('Error uploading photo:', err)
          });
        } else {
          void this.router.navigate(['/home']); // todo: naviagte to home with meals tab active
        }
      },
      error: (err) => console.error('Error creating meal:', err)
    });

  }
}
