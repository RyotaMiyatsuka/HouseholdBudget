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

  constructor(private authService: Auth) {
    this.registerForm = { invalid: false };
  }

  register() {
    this.authService.registerWithGoogle().subscribe({
      next: (response) => {
        if (response.success) {
          console.log('Registration successful:', response.message);
          console.log('User:', response.user);
          this.router.navigate(['/input']);
        }
      },
      error: (error) => {
        console.error('Registration failed:', error);
      }
    });
  }
}
