import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Auth } from '../../services/auth/auth';

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.html',
  styleUrl: './settings.scss'
})
export class Settings {
  private router = inject(Router);
  private authService = inject(Auth); // AuthServiceを注入

  logout() {
    // ログアウト処理をここに実装
    // 例: 認証サービスのlogoutメソッドを呼び出すなど
    console.log('ログアウトしました');
    this.authService.logout(); // Authサービスのlogoutメソッドを呼び出す
    this.router.navigate(['/']); // ログインページへリダイレクト
  }
}
