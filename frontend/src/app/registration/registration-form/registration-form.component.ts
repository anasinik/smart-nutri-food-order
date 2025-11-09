import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { RegistrationService } from '../registration.service';

@Component({
  selector: 'app-registration-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent {
  registrationForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private service: RegistrationService
  ) {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]] // TODO: add more validators to match backend validations
    });
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      var data = this.registrationForm.value;
      this.service.register(data).subscribe({
        next: (id) => {
          console.log("success" + id)
        },
        error: (err) => console.log(err)
      })
      console.log('Registration data:', this.registrationForm.value);
    } else {
      this.registrationForm.markAllAsTouched();
    }
  }
}
