import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-signin',
  imports: [RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  protected loginForm: { invalid: boolean }; // Placeholder for the actual form type
  constructor() {this.loginForm = { invalid: false }; }
  login() {
    console.log('Login button clicked');
  }
}
