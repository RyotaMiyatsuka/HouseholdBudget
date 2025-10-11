import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule],
  templateUrl: './input.html',
  styleUrl: './input.css'
})
export class Input {
  inputControl = new FormControl('');

  get value() {
    return this.inputControl.value;
  }

  // 金額登録
  register() {
    console.log(this.value);
  }

}
