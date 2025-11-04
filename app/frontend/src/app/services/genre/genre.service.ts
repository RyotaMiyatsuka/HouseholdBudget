import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../api/api-client.service';
import {
  Category,
  CategoryCreateRequest,
  CategoryUpdateRequest
} from '../../models/category.model';

/**
 * Category API Service
 * Provides methods to interact with category endpoints based on OpenAPI specification
 * Endpoint: /api/category
 */
@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private readonly endpoint = '/category';

  constructor(private apiClient: ApiClientService) { }

  /**
   * Get all categories
   * GET /api/category
   * @returns Observable of category array
   */
  listCategories(): Observable<Category[]> {
    return this.apiClient.get<Category[]>(this.endpoint);
  }

  /**
   * Create a new category
   * POST /api/category
   * @param category - Category data to create (only categoryName required)
   * @returns Observable of void (API returns 201 with no body)
   */
  createCategory(category: CategoryCreateRequest): Observable<void> {
    return this.apiClient.post<void>(this.endpoint, category);
  }

  /**
   * Update an existing category
   * PATCH /api/category
   * @param category - Category data to update (categoryId and categoryName required)
   * @returns Observable of updated category
   */
  updateCategory(category: CategoryUpdateRequest): Observable<Category> {
    return this.apiClient.patch<Category>(this.endpoint, category);
  }

  /**
   * Delete a category by ID
   * DELETE /api/category?id={categoryId}
   * @param id - Category ID to delete (UUID format)
   * @returns Observable of void (API returns 204 with no body)
   */
  deleteCategory(id: string): Observable<void> {
    return this.apiClient.delete<void>(this.endpoint, { id });
  }
}
