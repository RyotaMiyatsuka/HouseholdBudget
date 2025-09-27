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

  constructor(private authService: Auth) {
    this.loginForm = { invalid: false };
  }

  login() {
    this.authService.loginWithGoogle().subscribe({
      next: (response) => {
        if (response.success) {
          console.log('Login successful:', response.message);
          console.log('User:', response.user);
          this.router.navigate(['/input']);
        }
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
}
