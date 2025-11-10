import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { LoginResponse } from '../model/login-response.model';
import { ToastService } from '../../shared/toast/toast.service';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    RouterModule
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})
export class LoginFormComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private service: UserService,
    private router: Router,
    private toast: ToastService
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const data = this.loginForm.value;
    this.service.login(data).subscribe({
      next: (response: LoginResponse) => {
        console.log("LOGIN RESPONSE =", response);

        if (response.result.succeeded && response.token) {
          localStorage.setItem('jwt_token', response.token);
          this.router.navigate(['/restaurants-overview']);
          this.toast.show('Successfully logged in!', 'success');
        } else {
          // TODO: show proper error to user
          console.error(response.result.errors);
          this.toast.show("Login failed. Please try again.", 'error')
        }
      },
      error: (err) => this.toast.show('Login failed. Please try again.', 'error')
    });
  }

}
