import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Modal } from '../modal/modal';
import { ModalState } from '../../models/modal-state.model';
import { ExpenseGenre } from '../../models/expense-genre.model';
import { GenreColor, GENRE_COLOR_OPTIONS, getGenreButtonClasses } from '../../models/genre-color.model';
import { CommonModule } from '@angular/common';
import { GenreService } from '../../services/genre/genre.service';

type InputModalType = 'genre-form' | 'genre-upper-limit';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Modal, CommonModule],
  templateUrl: './input.html',
  styleUrl: './input.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Input implements OnInit {
  private readonly genreService = inject(GenreService);

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
    'add-button'
  ]);

  // Expose color options and utility function to template
  readonly colorOptions = GENRE_COLOR_OPTIONS;
  readonly getGenreButtonClasses = getGenreButtonClasses;

  ngOnInit() {
    this.loadGenres();
  }

  /**
   * Load genres from the API via GenreService
   */
  loadGenres() {
    this.genreService.getGenres().subscribe({
      next: (genres) => {
        this.genres.set([...genres, 'add-button']);
      },
      error: (error) => {
        console.error('Failed to load genres:', error);
      }
    });
  }

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
      const newGenreData = {
        name: newGenreName,
        color: selectedColor
      };

      this.genreService.createGenre(newGenreData).subscribe({
        next: (createdGenre) => {
          const currentGenres = this.genres();
          const genreObjects = currentGenres.filter(g => g !== 'add-button') as ExpenseGenre[];

          // Insert the new genre before the 'add-button'
          const updatedGenres: (ExpenseGenre | 'add-button')[] = [...genreObjects, createdGenre, 'add-button'];
          this.genres.set(updatedGenres);

          // Select the newly added genre
          this.selectedGenreIndex.set(genreObjects.length);

          this.closeModal();
          this.genreNameControl.setValue('');
          this.selectedColorControl.setValue(GenreColor.Green);
        },
        error: (error) => {
          console.error('Failed to create genre:', error);
          if (error.status === 400) {
            // Show upper limit modal if max genres reached
            this.showGenreUpperLimitModal();
          }
          this.closeModal();
        }
      });
    } else {
      this.closeModal();
      this.genreNameControl.setValue('');
      this.selectedColorControl.setValue(GenreColor.Green);
    }
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
