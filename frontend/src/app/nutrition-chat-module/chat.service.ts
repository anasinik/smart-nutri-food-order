import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from '../../env';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(
    private http: HttpClient,
  ) {}

  sendMessage(message: string): Observable<any> {
    return this.http.post(env + '/api/AI/question', { question: message });
  }
}
