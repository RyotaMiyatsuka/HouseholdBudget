import { Injectable } from '@angular/core';
import { delay, Observable, of, startWith } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

export interface HttpRequestState<T> {
  isLoading: boolean;
  success?: boolean;
  value?: T;
  error?: HttpErrorResponse | Error;
}

export interface User {
  id: string;
  email: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private currentUser: User | null = null;
  loginWithGoogle(): Observable<HttpRequestState<User>> {
    const mockUser: User = {
      id: 'mock-google-user-id',
      email: 'user@gmail.com',
      name: 'Mock Google User'
    };
    return of(
      { isLoading: false, success: true, value: mockUser, error: undefined }
    ).pipe(
      delay(Math.random() * 1000),
    );
  }

  registerWithGoogle(): Observable<HttpRequestState<User>> {
    const mockUser: User = {
      id: 'mock-google-register-id',
      email: 'newuser@gmail.com',
      name: 'New Google User'
    };
    return of(
      { isLoading: false, success: true, value: mockUser, error: undefined }
    ).pipe(
      delay(Math.random() * 1000),
    );
  }

  getCurrentUser(): User | null {
    return this.currentUser;
  }

  logout(): void {
    this.currentUser = null;
  }

  isAuthenticated(): boolean {
    return this.currentUser !== null;
  }
}
