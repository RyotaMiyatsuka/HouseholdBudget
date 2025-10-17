import { Component, signal, effect, computed, inject } from '@angular/core';
import { RouterModule, NavigationEnd, Router } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-bottom-nav',
  imports: [RouterModule],
  templateUrl: './bottom-nav.html',
  styleUrl: './bottom-nav.css'
})
export class BottomNav {
  readonly hiddenRoutes: string[] = [
    '/login',
    '/register',
    '/forgot-password',
    '/reset-password',
    '/'
  ];

  router = inject(Router);
  // Convert router events to signal
  private readonly currentRoute = toSignal(
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => this.router.url)
    ),
    { initialValue: this.router.url }
  );

  // Computed signal for visibility
  public readonly isVisible = computed(() => {
    return !this.hiddenRoutes.includes(this.currentRoute());
  });
}
