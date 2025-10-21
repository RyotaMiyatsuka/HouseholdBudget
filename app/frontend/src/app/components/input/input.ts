import { Component, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Modal } from '../modal/modal';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Modal],
  templateUrl: './input.html',
  styleUrl: './input.scss'
})
export class Input {
  inputControl = new FormControl('');
  genreNameControl = new FormControl('');
  modalOpen = false;

  // Convert to signal
  readonly selectedGenreIndex = signal(0);

  readonly genres = signal(['分類', '分類', '分類', '+']);

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

  addNewGenre(index: number) {
    this.selectedGenreIndex.set(index);
    this.modalOpen = true;
    this.genreNameControl.setValue('');
  }

  onConfirmGenre() {
    const newGenreName = this.genreNameControl.value?.trim();
    if (newGenreName) {
      const currentGenres = this.genres();
      const updatedGenres = [...currentGenres];
      // Insert the new genre before the '+' button
      updatedGenres.splice(currentGenres.length - 1, 0, newGenreName);
      this.genres.set(updatedGenres);
      // Select the newly added genre
      this.selectedGenreIndex.set(currentGenres.length - 1);
    }
    this.modalOpen = false;
    this.genreNameControl.setValue('');
  }

  onCancelGenre() {
    this.modalOpen = false;
    this.selectedGenreIndex.set(0);
    this.genreNameControl.setValue('');
  }

}
