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
          <button
            (click)="createSampleData()"
            class="bg-orange-500 text-white px-4 py-2 rounded hover:bg-orange-600">
            Create Sample Data (10 transactions)
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

  /**
   * Create sample transactions for testing
   * First creates a category, then creates multiple transactions
   */
  createSampleData() {
    this.clearMessages();
    console.log('Creating sample data...');

    // First, get existing categories or create one
    this.genreService.listCategories().subscribe({
      next: (categories) => {
        if (categories.length === 0) {
          // Create a default category first
          this.genreService.createCategory({ categoryName: '食費' }).subscribe({
            next: () => {
              this.genreService.listCategories().subscribe({
                next: (newCategories) => {
                  if (newCategories.length > 0) {
                    this.createMultipleTransactions(newCategories[0].categoryId);
                  }
                }
              });
            },
            error: (err) => {
              this.error.set('Failed to create category: ' + err.message);
            }
          });
        } else {
          // Use the first existing category
          this.createMultipleTransactions(categories[0].categoryId);
        }
      },
      error: (err) => {
        this.error.set('Failed to load categories: ' + err.message);
      }
    });
  }

  /**
   * Create multiple sample transactions for November 2025
   */
  private createMultipleTransactions(categoryId: string) {
    const sampleTransactions = [
      { amount: 1500, date: '2025-11-01', transactionType: 'expense' as const, memo: 'ランチ', place: 'レストランA' },
      { amount: 3200, date: '2025-11-02', transactionType: 'expense' as const, memo: '交通費', place: '駅' },
      { amount: 5000, date: '2025-11-03', transactionType: 'expense' as const, memo: '食料品', place: 'スーパー' },
      { amount: 50000, date: '2025-11-04', transactionType: 'income' as const, memo: '給料', place: '会社' },
      { amount: 2500, date: '2025-11-05', transactionType: 'expense' as const, memo: 'コーヒー', place: 'カフェ' },
      { amount: 8900, date: '2025-11-08', transactionType: 'expense' as const, memo: 'ディナー', place: '居酒屋' },
      { amount: 1200, date: '2025-11-10', transactionType: 'expense' as const, memo: '本', place: '書店' },
      { amount: 3500, date: '2025-11-12', transactionType: 'expense' as const, memo: 'ガソリン', place: 'ガソリンスタンド' },
      { amount: 15000, date: '2025-11-15', transactionType: 'income' as const, memo: 'ボーナス', place: '会社' },
      { amount: 6700, date: '2025-11-18', transactionType: 'expense' as const, memo: '衣服', place: 'デパート' }
    ];

    let completed = 0;
    let failed = 0;

    sampleTransactions.forEach((trans, index) => {
      const transaction = {
        ...trans,
        currency: 'JPY',
        categoryId
      };

      // Delay each request slightly to avoid overwhelming the server
      setTimeout(() => {
        this.transactionsService.createTransaction(transaction).subscribe({
          next: (data) => {
            completed++;
            console.log(`✅ Created transaction ${completed}/${sampleTransactions.length}:`, data);

            if (completed + failed === sampleTransactions.length) {
              this.success.set(`Created ${completed} transactions successfully!`);
              if (failed > 0) {
                this.error.set(`${failed} transactions failed`);
              }
              // Refresh the list
              this.testListTransactions();
            }
          },
          error: (err) => {
            failed++;
            console.error(`❌ Failed to create transaction ${index + 1}:`, err);

            if (completed + failed === sampleTransactions.length) {
              this.success.set(`Created ${completed} transactions`);
              this.error.set(`${failed} transactions failed: ` + err.message);
            }
          }
        });
      }, index * 200); // 200ms delay between each request
    });
  }

  private clearMessages() {
    this.error.set('');
    this.success.set('');
  }
}
