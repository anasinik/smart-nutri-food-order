import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-chat-widget',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './chat-widget.component.html',
  styleUrls: ['./chat-widget.component.css']
})
export class ChatWidgetComponent {
  showChat = false;
  newMessage = '';
  messages: { from: 'user' | 'ai'; text: string }[] = [
    { from: 'user', text: 'I want something healthy' },
    { from: 'ai', text: 'Try a quinoa salad with avocado!' }
  ];

  constructor(
    private chatService: ChatService
  ) {}

  toggleChat() {
    this.showChat = !this.showChat;
  }

  sendMessage() {
    if (!this.newMessage.trim()) return;
    this.messages.push({ from: 'user', text: this.newMessage });
    this.messages.push({ from: 'ai', text: 'AI is thinking...' });
    this.chatService.sendMessage(this.newMessage).subscribe({
      next: (response) => {
        console.log('AI response:', response);
        this.messages.push({ from: 'ai', text: response.text });
        this.newMessage = '';
      },
      error: (err) => {
        console.error('Error sending message:', err);
      },
      complete: () => {
        console.log('Message send completed');
      }
    });

  }
}
