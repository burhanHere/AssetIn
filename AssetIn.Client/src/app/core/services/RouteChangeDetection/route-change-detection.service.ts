import { inject, Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouteChangeDetectionService {
  private router: Router = inject(Router);
  private routeChangedSource: Subject<void>;
  public routeChanged: Observable<any>;

  constructor() {
    this.routeChangedSource = new Subject<void>();
    this.routeChanged = this.routeChangedSource.asObservable();
    this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(() => {
      this.routeChangedSource.next();
    });
  }
}
