import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

export interface User {
  id: string;
  email: string;
  name: string;
}

export interface AuthResponse {
  success: boolean;
  user?: User;
  message?: string;
}

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private currentUser: User | null = null;

  loginWithGoogle(): Observable<AuthResponse> {
    const mockUser: User = {
      id: 'mock-google-user-id',
      email: 'user@gmail.com',
      name: 'Mock Google User'
    };

    this.currentUser = mockUser;

    return of({
      success: true,
      user: mockUser,
      message: 'Successfully logged in with Google'
    });
  }

  registerWithGoogle(): Observable<AuthResponse> {
    const mockUser: User = {
      id: 'mock-google-register-id',
      email: 'newuser@gmail.com',
      name: 'New Google User'
    };

    this.currentUser = mockUser;

    return of({
      success: true,
      user: mockUser,
      message: 'Successfully registered with Google'
    });
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
