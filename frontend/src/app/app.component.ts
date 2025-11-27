import { Component, ViewChild } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { NavbarComponent } from './infrastructure/navbar/navbar.component';
import { MatIconModule } from '@angular/material/icon';
import { ChatWidgetComponent } from './nutrition-chat-module/chat-widget/chat-widget.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatButtonModule, NavbarComponent, MatIconModule, ChatWidgetComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'frontend';
  loggedIn = true;

  @ViewChild(ChatWidgetComponent) chatWidget!: ChatWidgetComponent;

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.loggedIn = !event.url.includes('login') && !event.url.includes('registration');
      }
    });
  }

  toggleChat() {
    this.chatWidget.toggleChat();
  }

}
