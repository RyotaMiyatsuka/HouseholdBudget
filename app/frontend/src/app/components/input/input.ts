import { Component, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule],
  templateUrl: './input.html',
  styleUrl: './input.css'
})
export class Input {
  inputControl = new FormControl('');

  // Convert to signal
  readonly selectedGenreIndex = signal(0);

  readonly genres = signal(['分類', '分類', '分類', '分類', '分類', '分類', '分類', '分類', '分類', '分類', '+']);

  selectGenre(index: number) {
    this.selectedGenreIndex.set(index);
  }

  get value() {
    return this.inputControl.value;
  }

  // 金額登録
  register() {
    console.log(this.value);
  }
}
