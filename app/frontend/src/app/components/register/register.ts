import { Component, inject } from '@angular/core';
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
  protected registerForm: { invalid: boolean }; // Placeholder for the actual form type
  buttonText = 'Sign up with Google';
  constructor(private authService: Auth) {
    this.registerForm = { invalid: false };
  }

  register() {
    this.buttonText = 'Signing up...';
    this.authService.registerWithGoogle().subscribe({
      next: (response) => {
        if (response.value) {
          this.buttonText = 'Sign in with Google';
          console.log('Registration successful:', response.value);
          console.log('User:', response.value);
          this.router.navigate(['/input']);
        }
      },
      error: (error) => {
        console.error('Registration failed:', error);
      }
    });
  }
}
