import { Component, inject, OnInit, } from '@angular/core';
import { nevbar } from '../../../core/constants/nevbar';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { JwtService } from '../../../core/services/jwt/jwt.service';
import { RouteChangeDetectionService } from '../../../core/services/RouteChangeDetection/route-change-detection.service';
@Component({
  selector: 'app-nevbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './nevbar.component.html',
  styleUrl: './nevbar.component.css',
})
export class NevbarComponent implements OnInit {
  private jwtServue: JwtService = inject(JwtService);
  private router: Router = inject(Router);
  private routeChangeDetectionService: RouteChangeDetectionService = inject(RouteChangeDetectionService);
  public nevbarOptions: Array<any>;
  public currentRole: string;
  private readonly NAVBAR_STORAGE_KEY = 'navbarOptions';

  constructor() {
    let token = sessionStorage.getItem('auth-jwt') || '';
    this.currentRole = this.jwtServue.getTokenClaims(token)['Role'];
    this.nevbarOptions = [];
  }

  ngOnInit(): void {
    // First, try to restore navbar options from storage
    this.restoreNavbarOptions();

    // Then update based on current route
    this.updateNavbarOptions();

    this.routeChangeDetectionService.routeChanged.subscribe(() => {
      this.updateNavbarOptions();
    });
  }

  private updateNavbarOptions(): void {
    const currentRoute = this.router.url.split('?')[0];

    // Skip update only if on settings AND we already have navbar options
    if (currentRoute.endsWith('settings') && this.nevbarOptions.length > 0) {
      return;
    }

    // Determine which route to use for navbar logic
    let routeForNavbar = currentRoute;

    // If on settings page and no navbar options, use stored route or default
    if (currentRoute.endsWith('settings')) {
      const storedRoute = sessionStorage.getItem('lastActiveRoute');
      routeForNavbar = storedRoute || this.getDefaultRouteForRole();
    } else {
      // Store the current route as the last active route
      sessionStorage.setItem('lastActiveRoute', currentRoute);
    }

    // Update navbar options based on the route
    if (routeForNavbar.endsWith('organizationsDashboard')) {
      this.nevbarOptions = nevbar.filter(i => i.path.endsWith('organizationsDashboard'));
    } else {
      this.nevbarOptions = this.initializeNevbarOptoins(nevbar);
    }

    // Store the navbar options for persistence
    this.storeNavbarOptions();
  }

  private restoreNavbarOptions(): void {
    const storedOptions = sessionStorage.getItem(this.NAVBAR_STORAGE_KEY);
    if (storedOptions) {
      try {
        this.nevbarOptions = JSON.parse(storedOptions);
      } catch (error) {
        console.error('Error parsing stored navbar options:', error);
        this.nevbarOptions = [];
      }
    }
  }

  private storeNavbarOptions(): void {
    sessionStorage.setItem(this.NAVBAR_STORAGE_KEY, JSON.stringify(this.nevbarOptions));
  }

  private getDefaultRouteForRole(): string {
    // Return a default route based on user role
    switch (this.currentRole) {
      case 'OrganizationOwner':
        return '/board/mainBoard/organizationOwner/organizationsDashboard';
      case 'OrganizationAssetManager':
        return '/board/mainBoard/organizationAssetManager/dashboard';
      case 'SuperAdmin':
        return '/board/mainBoard/superAdmin/dashboard';
      default:
        return '/board/mainBoard/dashboard';
    }
  }

  private initializeNevbarOptoins(nevbar: Array<any>): Array<any> {
    let options = nevbar;
    options = options.filter(i => i.acceptableRoles.includes(this.currentRole));
    return options;
  }
}
