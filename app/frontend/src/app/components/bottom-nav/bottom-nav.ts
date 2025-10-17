import { Component, signal, effect } from '@angular/core';
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

  // Convert router events to signal
  private readonly currentRoute = toSignal(
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd),
      map((event: NavigationEnd) => event.urlAfterRedirects)
    ),
    { initialValue: this.router.url }
  );

  // Computed signal for visibility
  public readonly isVisible = signal(false);

  constructor(private router: Router) {
    // Effect to update visibility when route changes
    effect(() => {
      const route = this.currentRoute();
      console.log('Current Route:', route);
      this.isVisible.set(!this.hiddenRoutes.includes(route));
    });
  }
}
