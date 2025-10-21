import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-modal',
  imports: [CommonModule],
  templateUrl: './modal.html',
  styleUrl: './modal.scss'
})
export class Modal {
  @Input() isOpen = false;
  @Input() title = '';
  @Input() showActions = true;
  @Input() confirmText = '確認';
  @Input() cancelText = 'キャンセル';
  @Input() confirmButtonClass = 'bg-main-blue hover:bg-main-blue-hover';
  @Input() cancelButtonClass = 'bg-gray-500 hover:bg-gray-600';

  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();
  @Output() close = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit();
    this.isOpen = false;
  }

  onCancel() {
    this.cancel.emit();
    this.isOpen = false;
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.close.emit();
      this.isOpen = false;
    }
  }
}
