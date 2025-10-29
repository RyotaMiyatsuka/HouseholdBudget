export type ModalState<T extends string = string> = {
  isOpen: boolean;
  title: string;
  type: "form" | "notification";
  message: string;
  confirmText: string;
  cancelText?: string;
};
