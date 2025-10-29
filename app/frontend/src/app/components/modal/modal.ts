import { ChangeDetectionStrategy, Component, EventEmitter, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalState } from '../../models/modal-state.model';

@Component({
  selector: 'app-modal',
  imports: [CommonModule],
  templateUrl: './modal.html',
  styleUrl: './modal.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Modal {
  isOpen = input<ModalState['isOpen']>(false);
  title = input<ModalState['title']>('');
  type = input<ModalState['type']>('form');
  confirmText = input<ModalState['confirmText']>('確認');
  message = input<ModalState['message']>('');
  cancelText = input<ModalState['cancelText']>('キャンセル');
  confirmButtonClass = input('bg-main-blue hover:bg-main-blue-hover');
  cancelButtonClass = input('bg-gray-500 hover:bg-gray-600');

  confirm = output<void>();
  cancel = output<void>();
  close = output<void>();

  onConfirm() {
    if (this.type() === 'form') {
      this.confirm.emit();
    } else if (this.type() === 'notification') {
      this.close.emit();
    }
  }

  onCancel() {
    this.cancel.emit();
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.close.emit();
    }
  }
}
