import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
  // Get JWT token from localStorage (you can modify this based on your auth strategy)
  const token = localStorage.getItem('jwt_token');

  // Clone the request and add authorization header if token exists
  let authReq = req;
  if (token) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  // Add common headers
  authReq = authReq.clone({
    setHeaders: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    }
  });

  const router = inject(Router);

  // Handle the request and catch errors
  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'An unknown error occurred';

      if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMessage = `Client Error: ${error.error.message}`;
      } else {
        // Server-side error
        switch (error.status) {
          case 400:
            errorMessage = 'Bad Request: ' + (error.error?.message || 'Invalid request');
            break;
          case 401:
            errorMessage = 'Unauthorized: Please login again';
            // Clear token and redirect to login
            localStorage.removeItem('jwt_token');
            router.navigate(['/login']);
            break;
          case 403:
            errorMessage = 'Forbidden: You do not have permission';
            break;
          case 404:
            errorMessage = 'Not Found: ' + (error.error?.message || 'Resource not found');
            break;
          case 422:
            errorMessage = 'Validation Error: ' + (error.error?.message || 'Invalid data');
            break;
          case 500:
            errorMessage = 'Server Error: Please try again later';
            break;
          default:
            errorMessage = `Error ${error.status}: ${error.error?.message || error.message}`;
        }
      }

      console.error('HTTP Error:', {
        status: error.status,
        message: errorMessage,
        error: error.error
      });

      return throwError(() => ({
        message: errorMessage,
        statusCode: error.status,
        timestamp: new Date().toISOString(),
        originalError: error
      }));
    })
  );
};
