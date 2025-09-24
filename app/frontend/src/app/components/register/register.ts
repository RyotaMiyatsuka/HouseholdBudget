import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  protected registerForm: { invalid: boolean }; // Placeholder for the actual form type
  constructor() {this.registerForm = { invalid: false }; }
  register() {
    console.log('Register button clicked');
  }
}
