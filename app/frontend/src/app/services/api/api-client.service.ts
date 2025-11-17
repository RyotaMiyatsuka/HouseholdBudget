import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

/**
 * Base API Client Service
 * Provides centralized HTTP methods with automatic base URL handling
 */
@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  /**
   * Performs a GET request
   * @param endpoint - API endpoint (without base URL)
   * @param params - Query parameters
   * @returns Observable of the response
   */
  get<T>(endpoint: string, params?: Record<string, any>): Observable<T> {
    const httpParams = this.buildHttpParams(params);
    return this.http.get<T>(`${this.baseUrl}${endpoint}`, { params: httpParams });
  }

  /**
   * Performs a POST request
   * @param endpoint - API endpoint (without base URL)
   * @param body - Request body
   * @returns Observable of the response
   */
  post<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}${endpoint}`, body);
  }

  /**
   * Performs a PATCH request
   * @param endpoint - API endpoint (without base URL)
   * @param body - Request body
   * @returns Observable of the response
   */
  patch<T>(endpoint: string, body: any): Observable<T> {
    return this.http.patch<T>(`${this.baseUrl}${endpoint}`, body);
  }

  /**
   * Performs a PUT request
   * @param endpoint - API endpoint (without base URL)
   * @param body - Request body
   * @returns Observable of the response
   */
  put<T>(endpoint: string, body: any): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}${endpoint}`, body);
  }

  /**
   * Performs a DELETE request
   * @param endpoint - API endpoint (without base URL)
   * @param params - Query parameters
   * @returns Observable of the response
   */
  delete<T>(endpoint: string, params?: Record<string, any>): Observable<T> {
    const httpParams = this.buildHttpParams(params);
    return this.http.delete<T>(`${this.baseUrl}${endpoint}`, { params: httpParams });
  }

  /**
   * Helper method to build HttpParams from an object
   * @param params - Object containing query parameters
   * @returns HttpParams instance
   */
  private buildHttpParams(params?: Record<string, any>): HttpParams {
    let httpParams = new HttpParams();

    if (params) {
      Object.keys(params).forEach(key => {
        const value = params[key];
        if (value !== null && value !== undefined) {
          httpParams = httpParams.set(key, String(value));
        }
      });
    }

    return httpParams;
  }
}
