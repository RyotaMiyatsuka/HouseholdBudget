import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [RouterModule, CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  //現在のURLに応じて、ヘッダーのログインボタンと登録ボタンの表示/非表示を制御する
  //locationの移動の際にヘッダーは初期化されないため、違う方法で現在のURLを管理する必要がある
  currentRoute: string;
  authButtonsVisibleRoutes: string[] = ['/login', '/register', '/forgot-password', '/reset-password', '/'];
  authButtonsVisible: boolean;

  constructor(private router: Router) {
    this.currentRoute = this.router.url;
    this.authButtonsVisible = this.authButtonsVisibleRoutes.includes(this.currentRoute);
    console.log('Current Route:', this.currentRoute);
  }
}
