import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Modal } from '../modal/modal';
import { ModalState } from '../../models/modal-state.model';
import { ExpenseGenre } from '../../models/expense-genre.model';
import { GenreColor, GENRE_COLOR_OPTIONS, getGenreButtonClasses } from '../../models/genre-color.model';
import { CommonModule } from '@angular/common';

type InputModalType = 'genre-form' | 'genre-upper-limit';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Modal, CommonModule],
  templateUrl: './input.html',
  styleUrl: './input.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Input {
  inputControl = new FormControl('');
  genreNameControl = new FormControl('');
  selectedColorControl = new FormControl<GenreColor>(GenreColor.Green);

  modalState = signal<ModalState<InputModalType>>({
    isOpen: false,
    title: '',
    type: 'form',
    confirmText: 'OK',
    message: '',
  });

  readonly selectedGenreIndex = signal(0);
  readonly genres = signal<(ExpenseGenre | 'add-button')[]>([
    { id: 1, name: '分類', color: GenreColor.Green },
    { id: 2, name: '分類', color: GenreColor.Blue },
    { id: 3, name: '分類', color: GenreColor.Red },
    { id: 4, name: '衣服', color: GenreColor.Purple },
    { id: 5, name: '食料品', color: GenreColor.Yellow },
    { id: 6, name: '娯楽費', color: GenreColor.Pink },
    { id: 7, name: 'その他', color: GenreColor.Orange },
    'add-button'
  ]);

  // Expose color options and utility function to template
  readonly colorOptions = GENRE_COLOR_OPTIONS;
  readonly getGenreButtonClasses = getGenreButtonClasses;

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

  addNewGenre() {
    const currentGenres = this.genres().filter(g => g !== 'add-button');
    if (currentGenres.length >= 8) {
      this.showGenreUpperLimitModal();
      return;
    }
    this.showGenreFormModal();
    this.genreNameControl.setValue('');
    this.selectedColorControl.setValue(GenreColor.Green);
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

  onModalCancel() {
    this.closeModal();
  }

  onConfirmGenre() {
    const newGenreName = this.genreNameControl.value?.trim();
    const selectedColor = this.selectedColorControl.value;

    if (newGenreName && selectedColor) {
      const currentGenres = this.genres();
      const genreObjects = currentGenres.filter(g => g !== 'add-button') as ExpenseGenre[];

      // Generate new ID
      const maxId = genreObjects.length > 0
        ? Math.max(...genreObjects.map(g => g.id))
        : 0;

      const newGenre: ExpenseGenre = {
        id: maxId + 1,
        name: newGenreName,
        color: selectedColor
      };

      // Insert the new genre before the 'add-button'
      const updatedGenres: (ExpenseGenre | 'add-button')[] = [...genreObjects, newGenre, 'add-button'];
      this.genres.set(updatedGenres);

      // Select the newly added genre
      this.selectedGenreIndex.set(genreObjects.length);
    }
    this.closeModal();
    this.genreNameControl.setValue('');
    this.selectedColorControl.setValue(GenreColor.Green);
  }

  closeModal() {
    this.modalState.update(state => ({ ...state, isOpen: false }));
    this.selectedGenreIndex.set(0);
    this.genreNameControl.setValue('');
    this.selectedColorControl.setValue(GenreColor.Green);
  }

  selectColor(color: GenreColor) {
    this.selectedColorControl.setValue(color);
  }

}
