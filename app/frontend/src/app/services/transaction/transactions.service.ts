import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../api/api-client.service';
import {
  Transaction,
  TransactionCreateRequest,
  TransactionUpdateRequest
} from '../../models/transaction.model';

/**
 * Transaction API Service
 * Provides methods to interact with transaction endpoints
 */
@Injectable({
  providedIn: 'root'
})
export class TransactionsService {
  private readonly endpoint = '/transactions';

  constructor(private apiClient: ApiClientService) { }

  /**
   * Get all transactions
   * @returns Observable of transaction array
   */
  listTransactions(): Observable<Transaction[]> {
    return this.apiClient.get<Transaction[]>(this.endpoint);
  }

  /**
   * Get transactions by month
   * @param year - Year to filter by
   * @param month - Month to filter by (1-12)
   * @returns Observable of transaction array
   */
  getTransactionsByMonth(year: number, month: number): Observable<Transaction[]> {
    return this.apiClient.get<Transaction[]>(`${this.endpoint}`, {
      year,
      month
    });
  }

  /**
   * Create a new transaction
   * @param transaction - Transaction data to create
   * @returns Observable of created transaction
   */
  createTransaction(transaction: TransactionCreateRequest): Observable<Transaction> {
    return this.apiClient.post<Transaction>(this.endpoint, transaction);
  }

  /**
   * Update an existing transaction
   * @param transaction - Transaction data to update
   * @returns Observable of updated transaction
   */
  updateTransaction(transaction: TransactionUpdateRequest): Observable<Transaction> {
    return this.apiClient.patch<Transaction>(this.endpoint, transaction);
  }

  /**
   * Delete a transaction by ID
   * @param id - Transaction ID to delete
   * @returns Observable of void
   */
  deleteTransaction(id: string): Observable<void> {
    return this.apiClient.delete<void>(this.endpoint, { id });
  }
}
