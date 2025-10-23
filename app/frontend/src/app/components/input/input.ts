import { Component, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Modal } from '../modal/modal';
import { ModalState } from '../../models/modal-state.model';

type InputModalType = 'genre-form' | 'genre-upper-limit';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Modal],
  templateUrl: './input.html',
  styleUrl: './input.scss'
})
export class Input {
  inputControl = new FormControl('');
  genreNameControl = new FormControl('');

  modalState = signal<ModalState<InputModalType>>({
    isOpen: false,
    title: '',
    type: 'form',
    confirmText: 'OK',
    message: '',
  });

  readonly selectedGenreIndex = signal(0);
  readonly genres = signal(['分類', '分類', '分類', '衣服', '食料品', '娯楽費', 'その他', '+']);

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
    if (this.genres().length >= 9) {
      this.showGenreUpperLimitModal();
      return;
    }
    this.showGenreFormModal();
    this.genreNameControl.setValue('');
  }

  showGenreFormModal() {
    this.modalState.set({
      isOpen: true,
      title: '新しいジャンルを追加',
      type: 'form',
      message: '',
      confirmText: '追加',
      cancelText: 'キャンセル'
    });
  }

  showGenreUpperLimitModal() {
    this.modalState.set({
      isOpen: true,
      title: 'ジャンル上限',
      type: 'notification',
      message: 'ジャンルは最大8つまで登録できます。',
      confirmText: 'OK',
    });
  }

  onModalConfirm() {
    const currentType = this.modalState().type;

    if (currentType === 'form') {
      this.onConfirmGenre();
    }
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
    this.closeModal();
    this.genreNameControl.setValue('');
  }

  closeModal() {
    this.modalState.update(state => ({ ...state, isOpen: false }));
    this.selectedGenreIndex.set(0);
    this.genreNameControl.setValue('');
  }

}
