import { Injectable, signal, computed } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, of, delay, tap, catchError, throwError } from 'rxjs';

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
  // Signals for reactive state management
  private readonly currentUser = signal<User | null>(null);
  readonly isAuthenticated = computed(() => this.currentUser() !== null);
  readonly loginState = signal<HttpRequestState<User>>({
    isLoading: false,
    success: undefined,
    value: undefined,
    error: undefined
  });

  // Expose readonly signal for components
  readonly currentUser$ = this.currentUser.asReadonly();

  // RxJS Observable-based login method
  loginWithGoogle(): Observable<User> {
    this.loginState.set({ isLoading: true });

    const mockUser: User = {
      id: 'mock-google-user-id',
      email: 'user@gmail.com',
      name: 'Mock Google User'
    };

    // Simulate HTTP request with Observable
    return of(mockUser).pipe(
      delay(Math.random() * 1000),
      tap(user => {
        // Update signals with success state
        this.currentUser.set(user);
        this.loginState.set({
          isLoading: false,
          success: true,
          value: user,
          error: undefined
        });
      }),
      catchError(error => {
        // Update signals with error state
        this.loginState.set({
          isLoading: false,
          success: false,
          value: undefined,
          error
        });
        return throwError(() => error);
      })
    );
  }

  // RxJS Observable-based register method
  registerWithGoogle(): Observable<User> {
    this.loginState.set({ isLoading: true });

    const mockUser: User = {
      id: 'mock-google-register-id',
      email: 'newuser@gmail.com',
      name: 'New Google User'
    };

    // Simulate HTTP request with Observable
    return of(mockUser).pipe(
      delay(Math.random() * 1000),
      tap(user => {
        // Update signals with success state
        this.currentUser.set(user);
        this.loginState.set({
          isLoading: false,
          success: true,
          value: user,
          error: undefined
        });
      }),
      catchError(error => {
        // Update signals with error state
        this.loginState.set({
          isLoading: false,
          success: false,
          value: undefined,
          error
        });
        return throwError(() => error);
      })
    );
  }

  getCurrentUser(): User | null {
    return this.currentUser();
  }

  logout(): void {
    this.currentUser.set(null);
    this.loginState.set({
      isLoading: false,
      success: undefined,
      value: undefined,
      error: undefined
    });
  }
}
