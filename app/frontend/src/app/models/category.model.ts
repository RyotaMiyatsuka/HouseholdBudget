export interface Category {
  categoryId: string;
  categoryName: string;
}

export interface CategoryCreateRequest {
  categoryName: string;
}

export interface CategoryUpdateRequest {
  categoryId: string;
  categoryName: string;
}
