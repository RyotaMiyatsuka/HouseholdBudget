import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { GenreService } from '../../services/genre/genre.service';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.html',
  styleUrl: './settings.scss'
})
export class Settings {
  private router = inject(Router);
  private authService = inject(AuthService);
  private genreService = inject(GenreService);
  readonly categories = signal<Category[]>([]);

  logout() {
    console.log('ログアウトしました');
    this.authService.logout();
    this.router.navigate(['/']);
  }

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.genreService.listCategories().subscribe({
      next: (categories) => {
        this.categories.set(categories);
      },
      error: (error) => {
        console.error('Failed to load categories:', error);
      }
    });
  }

  deleteCategory(categoryId: string) {
    this.genreService.deleteCategory(categoryId).subscribe({
      next: () => {
        console.log(`Category with ID ${categoryId} deleted successfully.`);
        this.loadCategories();
      },
      error: (error) => {
        console.error('Failed to delete category:', error);
      }
    });
  }
}
