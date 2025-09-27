import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-register',
  imports: [RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
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
        }
      },
      error: (error) => {
        console.error('Registration failed:', error);
      }
    });
  }
}
