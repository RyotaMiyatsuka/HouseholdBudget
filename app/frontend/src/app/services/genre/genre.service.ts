import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiClientService } from '../api/api-client.service';
import {
  Category,
  CategoryCreateRequest,
  CategoryUpdateRequest
} from '../../models/category.model';
import { ExpenseGenre } from '../../models/expense-genre.model';
import { GenreColor } from '../../models/genre-color.model';

/**
 * Category (Genre) API Service
 * Provides methods to interact with category endpoints based on OpenAPI specification
 */
@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private readonly endpoint = '/category';

  constructor(private apiClient: ApiClientService) {}

  // ========== NEW API METHODS (OpenAPI Spec) ==========

  /**
   * Get all categories
   * @returns Observable of category array
   */
  listCategories(): Observable<Category[]> {
    return this.apiClient.get<Category[]>(this.endpoint);
  }

  /**
   * Create a new category
   * @param category - Category data to create
   * @returns Observable of void (API returns 201 with no body)
   */
  createCategory(category: CategoryCreateRequest): Observable<void> {
    return this.apiClient.post<void>(this.endpoint, category);
  }

  /**
   * Update an existing category
   * @param category - Category data to update
   * @returns Observable of updated category
   */
  updateCategory(category: CategoryUpdateRequest): Observable<Category> {
    return this.apiClient.patch<Category>(this.endpoint, category);
  }

  /**
   * Delete a category by ID
   * @param id - Category ID to delete (UUID format)
   * @returns Observable of void (API returns 204 with no body)
   */
  deleteCategory(id: string): Observable<void> {
    return this.apiClient.delete<void>(this.endpoint, { id });
  }

  // ========== LEGACY METHODS (For backward compatibility) ==========
  // TODO: Remove these once all components are migrated to new API

  /**
   * @deprecated Use listCategories() instead
   * Legacy method for backward compatibility with ExpenseGenre model
   */
  getGenres(): Observable<ExpenseGenre[]> {
    // Mock data for now - in real implementation, this would map from Category to ExpenseGenre
    const mockGenres: ExpenseGenre[] = [
      { id: 1, name: '食費', color: GenreColor.Green },
      { id: 2, name: '交通費', color: GenreColor.Blue },
      { id: 3, name: '医療費', color: GenreColor.Red },
      { id: 4, name: '衣服', color: GenreColor.Purple },
      { id: 5, name: '食料品', color: GenreColor.Yellow },
      { id: 6, name: '娯楽費', color: GenreColor.Pink },
      { id: 7, name: 'その他', color: GenreColor.Orange },
    ];
    return of(mockGenres);
  }

  /**
   * @deprecated Use listCategories() and find by ID instead
   * Legacy method for backward compatibility
   */
  getGenreById(id: number): Observable<ExpenseGenre> {
    // Mock implementation
    const mockGenre: ExpenseGenre = { id, name: 'Mock Genre', color: GenreColor.Green };
    return of(mockGenre);
  }

  /**
   * @deprecated Use createCategory() instead
   * Legacy method for backward compatibility
   */
  createGenre(genre: Omit<ExpenseGenre, 'id'>): Observable<ExpenseGenre> {
    // Mock implementation
    const mockGenre: ExpenseGenre = { id: Date.now(), ...genre };
    return of(mockGenre);
  }

  /**
   * @deprecated Use updateCategory() instead
   * Legacy method for backward compatibility
   */
  updateGenre(id: number, genre: Partial<ExpenseGenre>): Observable<ExpenseGenre> {
    // Mock implementation
    const mockGenre: ExpenseGenre = { id, name: genre.name || '', color: genre.color || GenreColor.Green };
    return of(mockGenre);
  }

  /**
   * @deprecated Use deleteCategory() instead
   * Legacy method for backward compatibility
   */
  deleteGenre(id: number): Observable<void> {
    // Mock implementation
    return of(void 0);
  }
}
