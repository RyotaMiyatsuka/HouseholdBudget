export type TransactionType = 'income' | 'expense';

export interface Transaction {
  id: string;
  amount: number;
  currency: string;
  date: string;
  transactionType: TransactionType;
  categoryId: string;
  memo?: string;
  place?: string;
}

export interface TransactionCreateRequest {
  amount: number;
  currency: string;
  date: string;
  transactionType: TransactionType;
  categoryId: string;
  memo?: string;
  place?: string;
}

export interface TransactionUpdateRequest {
  id: string;
  amount?: number;
  currency?: string;
  date?: string;
  transactionType?: TransactionType;
  categoryId?: string;
  memo?: string;
  place?: string;
}
