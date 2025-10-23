import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Modal } from './modal';

describe('Modal', () => {
  let component: Modal;
  let fixture: ComponentFixture<Modal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Modal]
    })
      .compileComponents();

    fixture = TestBed.createComponent(Modal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should emit confirm event and close the modal on confirm', () => {
    vi.spyOn(component.confirm, 'emit');
    component.isOpen = true;
    component.onConfirm();
    expect(component.confirm.emit).toHaveBeenCalled();
    expect(component.isOpen).toBeFalsy();
  });

  it('should emit cancel event and close the modal on cancel', () => {
    vi.spyOn(component.cancel, 'emit');
    component.isOpen = true;
    component.onCancel();
    expect(component.cancel.emit).toHaveBeenCalled();
    expect(component.isOpen).toBeFalsy();
  });

});
