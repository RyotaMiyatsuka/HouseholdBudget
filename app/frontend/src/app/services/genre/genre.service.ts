import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ExpenseGenre } from '../../models/expense-genre.model';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = '/api/genres';

  /**
   * Get all expense genres
   */
  getGenres(): Observable<ExpenseGenre[]> {
    return this.http.get<ExpenseGenre[]>(this.apiUrl);
  }

  /**
   * Get a single genre by ID
   */
  getGenreById(id: number): Observable<ExpenseGenre> {
    return this.http.get<ExpenseGenre>(`${this.apiUrl}/${id}`);
  }

  /**
   * Create a new genre
   */
  createGenre(genre: Omit<ExpenseGenre, 'id'>): Observable<ExpenseGenre> {
    return this.http.post<ExpenseGenre>(this.apiUrl, genre);
  }

  /**
   * Update an existing genre
   */
  updateGenre(id: number, genre: Partial<ExpenseGenre>): Observable<ExpenseGenre> {
    return this.http.put<ExpenseGenre>(`${this.apiUrl}/${id}`, genre);
  }

  /**
   * Delete a genre
   */
  deleteGenre(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
