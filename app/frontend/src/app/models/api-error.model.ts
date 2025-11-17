export interface ApiError {
  message: string;
  statusCode: number;
  timestamp: string;
  path?: string;
}

export interface ApiErrorResponse {
  error: ApiError;
}
