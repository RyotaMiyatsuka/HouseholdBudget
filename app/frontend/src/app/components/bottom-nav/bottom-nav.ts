import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterModule, NavigationEnd, Router } from '@angular/router';
import { filter, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-bottom-nav',
  imports: [RouterModule],
  templateUrl: './bottom-nav.html',
  styleUrl: './bottom-nav.css'
})
export class BottomNav {
  private currentRoute: string = '';
  public isVisible: boolean = false;
  private readonly destroy$ = new Subject<void>();
  readonly hiddenRoutes: string[] = [
    '/login',
    '/register',
    '/forgot-password',
    '/reset-password',
    '/'
  ];

  constructor(private router: Router) { }
  ngOnInit(): void {
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd),
        takeUntil(this.destroy$)
      )
      .subscribe((event: NavigationEnd) => {
        this.currentRoute = event.urlAfterRedirects;
        console.log('Current Route:', this.currentRoute);
        this.isVisible = !this.hiddenRoutes.includes(this.currentRoute);
      });
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
