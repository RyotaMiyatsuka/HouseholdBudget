import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Modal } from '../modal/modal';
import { ModalState } from '../../models/modal-state.model';
import { Category } from '../../models/category.model';
import { CommonModule } from '@angular/common';
import { GenreService } from '../../services/genre/genre.service';

type InputModalType = 'form' | 'notification';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [ReactiveFormsModule, Modal, CommonModule],
  templateUrl: './input.html',
  styleUrls: ['./input.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Input implements OnInit {
  private readonly genreService = inject(GenreService);
  inputControl = new FormControl('');
  categoryNameControl = new FormControl('',
    [Validators.required, Validators.maxLength(20)]
  );

  modalState = signal<ModalState<InputModalType>>({
    isOpen: false,
    title: '',
    type: 'form',
    confirmText: 'OK',
    message: '',
  });

  readonly selectedCategoryIndex = signal(0);
  readonly categories = signal<(Category | 'add-button')[]>([
    'add-button'
  ]);

  ngOnInit() {
    this.loadCategories();
  }

  /**
   * Load categories from the API via GenreService
   */
  loadCategories() {
    this.genreService.listCategories().subscribe({
      next: (categories) => {
        this.categories.set([...categories, 'add-button']);
      },
      error: (error) => {
        console.error('Failed to load categories:', error);
      }
    });
  }

  selectCategory(index: number) {
    this.selectedCategoryIndex.set(index);
  }

  get value() {
    return this.inputControl.value;
  }

  // 金額登録
  register() {
    console.log(this.value);
  }

  addNewCategory() {
    const currentCategories = this.categories().filter(c => c !== 'add-button');
    if (currentCategories.length >= 8) {
      this.showCategoryUpperLimitModal();
      return;
    }
    this.showCategoryFormModal();
    this.categoryNameControl.setValue('');
  }

  showCategoryFormModal() {
    this.modalState.set({
      isOpen: true,
      title: '新しいカテゴリを追加',
      type: 'form',
      message: '',
      confirmText: '追加',
      cancelText: 'キャンセル'
    });
  }

  showCategoryUpperLimitModal() {
    this.modalState.set({
      isOpen: true,
      title: 'カテゴリ上限',
      type: 'notification',
      message: 'カテゴリは最大8つまで登録できます。',
      confirmText: 'OK',
    });
  }

  onModalConfirm() {
    const currentType = this.modalState().type;
    if (currentType === 'form') {
      this.onConfirmCategory();
    }
  }

  onModalCancel() {
    this.closeModal();
  }

  onConfirmCategory() {
    const newCategoryName = this.categoryNameControl.value?.trim();

    if (!newCategoryName || newCategoryName.length === 0) {
      this.categoryNameControl.markAsTouched();
      this.categoryNameControl.setErrors({ required: true });
      this.categoryNameControl.setValue('');
      return;
    }

    if (this.categoryNameControl.invalid) {
      return;
    }

    const newCategoryData = {
      categoryName: newCategoryName
    };

    this.genreService.createCategory(newCategoryData).subscribe({
      next: () => {
        // Reload categories from server to get the new category with its ID
        this.loadCategories();
        this.closeModal();
        this.categoryNameControl.setValue('');
      },
      error: (error) => {
        console.error('Failed to create category:', error);
        if (error.status === 400) {
          // Show upper limit modal if max categories reached
          this.showCategoryUpperLimitModal();
        }
        this.closeModal();
      }
    });
  }

  closeModal() {
    this.modalState.update(state => ({ ...state, isOpen: false }));
    this.selectedCategoryIndex.set(0);
    this.categoryNameControl.setValue('');
    this.categoryNameControl.markAsUntouched();
  }

  /**
   * Get the error message for category name input
   * @returns Error message string or null if no error
   */
  getCategoryNameError(): string | null {
    if (!this.categoryNameControl.touched || this.categoryNameControl.valid) {
      return null;
    }

    if (this.categoryNameControl.hasError('required')) {
      return 'カテゴリ名は必須です';
    }

    if (this.categoryNameControl.hasError('maxlength')) {
      return '最大20文字です';
    }

    return null;
  }

}
