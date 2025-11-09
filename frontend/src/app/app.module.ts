import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './infrastructure/material/material.module';
import { HomeModule } from './home/home.module';
import { AppRoutingModule } from './app-routing.module';
import { RegistrationModule } from './registration/registration.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HomeModule,
    RegistrationModule
  ]
})
export class AppModule { }
