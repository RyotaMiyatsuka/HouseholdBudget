import { Injectable, signal, computed } from '@angular/core';
import { Observable, of, delay, tap, catchError, throwError } from 'rxjs';
import { User } from '../../models/user.model';
import { HttpRequestState } from '../../models/http-request-state.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
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
