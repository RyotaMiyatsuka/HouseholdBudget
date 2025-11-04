import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsService } from '../../services/transaction/transactions.service';
import { GenreService } from '../../services/genre/genre.service';
import { Transaction } from '../../models/transaction.model';
import { Category } from '../../models/category.model';

/**
 * Test component to verify API integration
 */
@Component({
  selector: 'app-api-test',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="p-4 max-w-4xl mx-auto">
      <h1 class="text-2xl font-bold mb-6">API Integration Test</h1>

      <!-- Transactions Section -->
      <div class="mb-8 border p-4 rounded">
        <h2 class="text-xl font-semibold mb-4">Transactions API</h2>

        <div class="space-x-2 mb-4">
          <button
            (click)="testListTransactions()"
            class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
            List Transactions
          </button>
          <button
            (click)="testGetByMonth()"
            class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600">
            Get Nov 2025
          </button>
          <button
            (click)="testCreateTransaction()"
            class="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600">
            Create Transaction
          </button>
        </div>

        @if (transactions().length > 0) {
          <div class="bg-gray-100 p-3 rounded">
            <h3 class="font-semibold mb-2">Transactions ({{ transactions().length }}):</h3>
            @for (trans of transactions(); track trans.id) {
              <div class="bg-white p-2 mb-2 rounded text-sm">
                <div><strong>ID:</strong> {{ trans.id }}</div>
                <div><strong>Amount:</strong> {{ trans.amount }} {{ trans.currency }}</div>
                <div><strong>Type:</strong> {{ trans.transactionType }}</div>
                <div><strong>Date:</strong> {{ trans.date }}</div>
                <div><strong>Memo:</strong> {{ trans.memo }}</div>
              </div>
            }
          </div>
        }
      </div>

      <!-- Categories Section -->
      <div class="mb-8 border p-4 rounded">
        <h2 class="text-xl font-semibold mb-4">Categories API</h2>

        <div class="space-x-2 mb-4">
          <button
            (click)="testListCategories()"
            class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
            List Categories
          </button>
          <button
            (click)="testCreateCategory()"
            class="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600">
            Create Category
          </button>
        </div>

        @if (categories().length > 0) {
          <div class="bg-gray-100 p-3 rounded">
            <h3 class="font-semibold mb-2">Categories ({{ categories().length }}):</h3>
            @for (cat of categories(); track cat.categoryId) {
              <div class="bg-white p-2 mb-2 rounded text-sm">
                <div><strong>ID:</strong> {{ cat.categoryId }}</div>
                <div><strong>Name:</strong> {{ cat.categoryName }}</div>
              </div>
            }
          </div>
        }
      </div>

      <!-- Error Display -->
      @if (error()) {
        <div class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4">
          <strong>Error:</strong> {{ error() }}
        </div>
      }

      <!-- Success Display -->
      @if (success()) {
        <div class="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded">
          <strong>Success:</strong> {{ success() }}
        </div>
      }
    </div>
  `
})
export class ApiTestComponent {
  transactions = signal<Transaction[]>([]);
  categories = signal<Category[]>([]);
  error = signal<string>('');
  success = signal<string>('');

  constructor(
    private transactionsService: TransactionsService,
    private genreService: GenreService
  ) {}

  testListTransactions() {
    this.clearMessages();
    console.log('Testing listTransactions()...');

    this.transactionsService.listTransactions().subscribe({
      next: (data) => {
        console.log('✅ List transactions success:', data);
        this.transactions.set(data);
        this.success.set('Loaded ' + data.length + ' transactions');
      },
      error: (err) => {
        console.error('❌ List transactions error:', err);
        this.error.set(err.message || 'Failed to load transactions');
      }
    });
  }

  testGetByMonth() {
    this.clearMessages();
    console.log('Testing getTransactionsByMonth(2025, 11)...');

    this.transactionsService.getTransactionsByMonth(2025, 11).subscribe({
      next: (data) => {
        console.log('✅ Get by month success:', data);
        this.transactions.set(data);
        this.success.set('Loaded ' + data.length + ' transactions for Nov 2025');
      },
      error: (err) => {
        console.error('❌ Get by month error:', err);
        this.error.set(err.message || 'Failed to load transactions by month');
      }
    });
  }

  testCreateTransaction() {
    this.clearMessages();
    console.log('Testing createTransaction()...');

    const newTransaction = {
      amount: 2500,
      currency: 'JPY',
      date: '2025-11-04',
      transactionType: 'expense' as const,
      categoryId: 'cat-1',
      memo: 'Test transaction',
      place: 'Test place'
    };

    this.transactionsService.createTransaction(newTransaction).subscribe({
      next: (data) => {
        console.log('✅ Create transaction success:', data);
        this.success.set('Created transaction: ' + data.id);
        // Refresh list
        this.testListTransactions();
      },
      error: (err) => {
        console.error('❌ Create transaction error:', err);
        this.error.set(err.message || 'Failed to create transaction');
      }
    });
  }

  testListCategories() {
    this.clearMessages();
    console.log('Testing listCategories()...');

    this.genreService.listCategories().subscribe({
      next: (data) => {
        console.log('✅ List categories success:', data);
        this.categories.set(data);
        this.success.set('Loaded ' + data.length + ' categories');
      },
      error: (err) => {
        console.error('❌ List categories error:', err);
        this.error.set(err.message || 'Failed to load categories');
      }
    });
  }

  testCreateCategory() {
    this.clearMessages();
    console.log('Testing createCategory()...');

    const newCategory = {
      categoryName: 'Test Category ' + Date.now()
    };

    this.genreService.createCategory(newCategory).subscribe({
      next: () => {
        console.log('✅ Create category success');
        this.success.set('Created category: ' + newCategory.categoryName);
        // Refresh list
        this.testListCategories();
      },
      error: (err) => {
        console.error('❌ Create category error:', err);
        this.error.set(err.message || 'Failed to create category');
      }
    });
  }

  private clearMessages() {
    this.error.set('');
    this.success.set('');
  }
}
