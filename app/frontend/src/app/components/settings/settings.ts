import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { GenreService } from '../../services/genre/genre.service';
import { ExpenseGenre } from '../../models/expense-genre.model';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.html',
  styleUrl: './settings.scss'
})
export class Settings {
  private router = inject(Router);
  private authService = inject(AuthService); // AuthServiceを注入
  private genreService = inject(GenreService);
  readonly genres = signal<(ExpenseGenre)[]>([]);

  logout() {
    // ログアウト処理をここに実装
    // 例: 認証サービスのlogoutメソッドを呼び出すなど
    console.log('ログアウトしました');
    this.authService.logout(); // Authサービスのlogoutメソッドを呼び出す
    this.router.navigate(['/']); // ログインページへリダイレクト
  }

  ngOnInit() {
    this.loadGenres();
  }

  loadGenres() {
    this.genreService.getGenres().subscribe({
      next: (genres) => {
        this.genres.set(genres);
      },
      error: (error) => {
        console.error('Failed to load genres:', error);
      }
    });
  }

  deleteGenre(genreId: number) {
    this.genreService.deleteGenre(genreId).subscribe({
      next: () => {
        console.log(`Genre with ID ${genreId} deleted successfully.`);
        this.loadGenres(); // ジャンルリストを再読み込み
      },
      error: (error) => {
        console.error('Failed to delete genre:', error);
      }
    });
  }
}
