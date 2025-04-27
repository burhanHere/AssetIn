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
export class NevbarComponent {
  private jwtServue: JwtService = inject(JwtService);
  private router: Router = inject(Router);
  private routeChangeDetectionService: RouteChangeDetectionService = inject(RouteChangeDetectionService);
  public nevbarOptions: Array<any>;
  public currentRole: string;

  constructor() {
    let token = sessionStorage.getItem('auth-jwt') || '';
    this.currentRole = this.jwtServue.getTokenClaims(token)['Role'];
    this.nevbarOptions = [];
    this.routeChangeDetectionService.routeChanged.subscribe(() => {
      const currentRoute = this.router.url.split('?')[0];
      if (currentRoute.endsWith('organizationsDashboard')) {
        this.nevbarOptions = nevbar.filter(i => i.path.endsWith('organizationsDashboard'));
      } else {
        this.nevbarOptions = this.initializeNevbarOptoins(nevbar);
      }
    });
  }

  private initializeNevbarOptoins(nevbar: Array<any>): Array<any> {
    let options = nevbar;
    options = options.filter(i => i.acceptableRoles.includes(this.currentRole));
    return options;
  }
}
