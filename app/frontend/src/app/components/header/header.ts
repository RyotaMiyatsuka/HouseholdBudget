import { Component, signal, effect } from '@angular/core';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-header',
  imports: [RouterModule, CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  private readonly authButtonsVisibleRoutes: string[] = [
    '/login',
    '/register',
    '/forgot-password',
    '/reset-password',
    '/'
  ];

  // Convert router events to signal
  readonly currentRoute = toSignal(
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd),
      map((event: NavigationEnd) => event.urlAfterRedirects)
    ),
    { initialValue: this.router.url }
  );

  // Signal for auth buttons visibility
  readonly authButtonsVisible = signal(false);

  constructor(private router: Router) {
    // Effect to update auth buttons visibility when route changes
    effect(() => {
      const route = this.currentRoute();
      this.authButtonsVisible.set(this.authButtonsVisibleRoutes.includes(route));
    });
  }
}
