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

  selectedGenreIndex = 0;

  selectGenre(index: number) {
    this.selectedGenreIndex = index;
  }

  genres = ['分類', '分類', '分類', '分類', '分類', '分類', '分類', '分類', '分類', '分類', '+'];

  get value() {
    return this.inputControl.value;
  }

  // 金額登録
  register() {
    console.log(this.value);
  }

}
