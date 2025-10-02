import { inject } from '@angular/core';
import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-signin',
  imports: [RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private router = inject(Router);
  protected loginForm: { invalid: boolean }; // Placeholder for the actual form type
  buttonText = 'Sign in with Google';
  constructor(private authService: Auth) {
    this.loginForm = { invalid: false };
  }

  login() {
    this.buttonText = 'Signing in...';
    this.authService.loginWithGoogle().subscribe({
      next: (response) => {
        if (response.value) {
          this.buttonText = 'Sign in with Google';
          console.log('Login successful:', response.value);
          console.log('User:', response.value);
          this.router.navigate(['/input']);
        }
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
}
