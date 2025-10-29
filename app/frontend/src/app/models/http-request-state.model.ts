import { HttpErrorResponse } from '@angular/common/http';

export type HttpRequestState<T> = {
  isLoading: boolean;
  success?: boolean;
  value?: T;
  error?: HttpErrorResponse | Error;
};
