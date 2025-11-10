import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './infrastructure/material/material.module';
import { HomeModule } from './home/home.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserModule } from './user/user.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HomeModule,
    UserModule
  ]
})
export class AppModule { }
