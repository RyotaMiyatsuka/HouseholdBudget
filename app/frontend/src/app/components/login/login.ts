import { inject, Component, signal, effect } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { Auth } from '../../services/auth/auth';

@Component({
  selector: 'app-signin',
  imports: [RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private router = inject(Router);
  private authService = inject(Auth);

  protected loginForm = signal({ invalid: false });
  readonly buttonText = signal('Sign in with Google');

  // Use effect to reactively watch loginState signal
  constructor() {
    effect(() => {
      const state = this.authService.loginState();
      if (state.success && state.value) {
        this.buttonText.set('Sign in with Google');
        console.log('Login successful:', state.value);
        this.router.navigate(['/input']);
      } else if (state.error) {
        console.error('Login failed:', state.error);
        this.buttonText.set('Sign in with Google');
      }
    });
  }

  login() {
    this.buttonText.set('Signing in...');
    // Subscribe to Observable, signals are updated via tap() in service
    this.authService.loginWithGoogle().subscribe({
      error: (error) => {
        console.error('Login error:', error);
      }
    });
  }
}
