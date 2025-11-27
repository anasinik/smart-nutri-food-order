import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-checkout-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './order-checkout-dialog.component.html',
  styleUrls: ['./order-checkout-dialog.component.css']
})
export class OrderCheckoutDialogComponent {

  checkoutForm: FormGroup;

  paymentMethods = [
    { value: 'cash', label: 'Cash' },
    { value: 'card', label: 'Card' }
  ];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<OrderCheckoutDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.checkoutForm = this.fb.group({
      address: ['', Validators.required],
      paymentMethod: ['', Validators.required]
    });
  }

  submit() {
    if (this.checkoutForm.valid) {
      this.dialogRef.close(this.checkoutForm.value);
    }
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
