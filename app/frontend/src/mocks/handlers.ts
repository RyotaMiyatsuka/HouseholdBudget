import { http, HttpResponse } from 'msw';
import { Transaction, TransactionCreateRequest } from '../app/models/transaction.model';
import { Category, CategoryCreateRequest } from '../app/models/category.model';

// Mock data for categories
let mockCategories: Category[] = [
  { categoryId: 'cat-1', categoryName: '食費' },
  { categoryId: 'cat-2', categoryName: '交通費' },
  { categoryId: 'cat-3', categoryName: '医療費' },
  { categoryId: 'cat-4', categoryName: '娯楽費' },
];

// Mock data for transactions
let mockTransactions: Transaction[] = [
  {
    id: 'trans-1',
    amount: 1500,
    currency: 'JPY',
    date: '2025-11-01',
    transactionType: 'expense',
    categoryId: 'cat-1',
    memo: 'ランチ',
    place: 'レストランA'
  },
  {
    id: 'trans-2',
    amount: 50000,
    currency: 'JPY',
    date: '2025-11-02',
    transactionType: 'income',
    categoryId: 'cat-1',
    memo: '給料',
    place: '会社'
  },
];

// Define your mock API handlers here - matching OpenAPI spec
export const handlers = [
  // ========== TRANSACTIONS ==========

  // GET /api/transactions - List all transactions
  http.get('http://localhost:8080/api/transactions', ({ request }) => {
    const url = new URL(request.url);
    const year = url.searchParams.get('year');
    const month = url.searchParams.get('month');

    // If filtering by month
    if (year && month) {
      const filtered = mockTransactions.filter(t => {
        const date = new Date(t.date);
        return date.getFullYear() === Number(year) && date.getMonth() + 1 === Number(month);
      });
      return HttpResponse.json(filtered);
    }

    return HttpResponse.json(mockTransactions);
  }),

  // GET /api/transactions/by-month - Get transactions by month
  http.get('http://localhost:8080/api/transactions/by-month', ({ request }) => {
    const url = new URL(request.url);
    const year = Number(url.searchParams.get('year'));
    const month = Number(url.searchParams.get('month'));

    const filtered = mockTransactions.filter(t => {
      const date = new Date(t.date);
      return date.getFullYear() === year && date.getMonth() + 1 === month;
    });

    return HttpResponse.json(filtered);
  }),

  // POST /api/transactions - Create transaction
  http.post('http://localhost:8080/api/transactions', async ({ request }) => {
    const body = await request.json() as TransactionCreateRequest;

    const newTransaction: Transaction = {
      id: `trans-${Date.now()}`,
      ...body
    };

    mockTransactions.push(newTransaction);
    return HttpResponse.json(newTransaction, { status: 201 });
  }),

  // PATCH /api/transactions - Update transaction
  http.patch('http://localhost:8080/api/transactions', async ({ request }) => {
    const body = await request.json() as any;
    const index = mockTransactions.findIndex(t => t.id === body.id);

    if (index === -1) {
      return HttpResponse.json(
        { message: 'Transaction not found' },
        { status: 404 }
      );
    }

    mockTransactions[index] = { ...mockTransactions[index], ...body };
    return HttpResponse.json(mockTransactions[index]);
  }),

  // DELETE /api/transactions - Delete transaction
  http.delete('http://localhost:8080/api/transactions', ({ request }) => {
    const url = new URL(request.url);
    const id = url.searchParams.get('id');

    const index = mockTransactions.findIndex(t => t.id === id);

    if (index === -1) {
      return HttpResponse.json(
        { message: 'Transaction not found' },
        { status: 404 }
      );
    }

    mockTransactions.splice(index, 1);
    return new HttpResponse(null, { status: 204 });
  }),

  // ========== CATEGORIES ==========

  // GET /api/category - List all categories
  http.get('http://localhost:8080/api/category', () => {
    return HttpResponse.json(mockCategories);
  }),

  // POST /api/category - Create category
  http.post('http://localhost:8080/api/category', async ({ request }) => {
    const body = await request.json() as CategoryCreateRequest;

    const newCategory: Category = {
      categoryId: `cat-${Date.now()}`,
      categoryName: body.categoryName
    };

    mockCategories.push(newCategory);
    return new HttpResponse(null, { status: 201 });
  }),

  // PATCH /api/category - Update category
  http.patch('http://localhost:8080/api/category', async ({ request }) => {
    const body = await request.json() as any;
    const index = mockCategories.findIndex(c => c.categoryId === body.categoryId);

    if (index === -1) {
      return HttpResponse.json(
        { message: 'Category not found' },
        { status: 404 }
      );
    }

    mockCategories[index] = { ...mockCategories[index], ...body };
    return HttpResponse.json(mockCategories[index]);
  }),

  // DELETE /api/category - Delete category
  http.delete('http://localhost:8080/api/category', ({ request }) => {
    const url = new URL(request.url);
    const id = url.searchParams.get('id');

    const index = mockCategories.findIndex(c => c.categoryId === id);

    if (index === -1) {
      return HttpResponse.json(
        { message: 'Category not found' },
        { status: 404 }
      );
    }

    mockCategories.splice(index, 1);
    return new HttpResponse(null, { status: 204 });
  }),
];
