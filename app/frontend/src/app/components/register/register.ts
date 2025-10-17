import { Component, inject, signal, effect } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-register',
  imports: [RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private router = inject(Router);
  private authService = inject(Auth);

  protected registerForm = signal({ invalid: false });
  readonly buttonText = signal('Sign up with Google');

  // Use effect to reactively watch loginState signal
  constructor() {
    effect(() => {
      const state = this.authService.loginState();
      if (state.success && state.value) {
        this.buttonText.set('Sign up with Google');
        console.log('Registration successful:', state.value);
        this.router.navigate(['/input']);
      } else if (state.error) {
        console.error('Registration failed:', state.error);
        this.buttonText.set('Sign up with Google');
      }
    });
  }

  register() {
    this.buttonText.set('Signing up...');
    // Subscribe to Observable, signals are updated via tap() in service
    this.authService.registerWithGoogle().subscribe({
      error: (error) => {
        console.error('Registration error:', error);
      }
    });
  }
}
