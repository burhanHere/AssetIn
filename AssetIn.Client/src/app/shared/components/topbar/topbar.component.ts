import {
  Component,
  inject,
} from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PageLoaderComponent } from '../page-loader/page-loader.component';
import { AlertCardComponent } from '../alert-card/alert-card.component';
import { RouteChangeDetectionService } from '../../../core/services/RouteChangeDetection/route-change-detection.service';
import { JwtService } from '../../../core/services/jwt/jwt.service';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.css',
})
export class TopbarComponent {
  private router: Router = inject(Router);
  private routeChangeDetectionService: RouteChangeDetectionService = inject(
    RouteChangeDetectionService
  );
  public jwtService: JwtService = inject(JwtService);

  public showSearchBar: boolean = false;
  public profilePicture: string;
  constructor() {
    this.routeChangeDetectionService.routeChanged.subscribe(() => {
      this.showSearchBar = this.router.url.includes(
        '/board/mainBoard/organizationAdmin'
      );
    });
    const jwt = sessionStorage.getItem('auth-jwt') || '';
    const claims = this.jwtService.getTokenClaims(jwt);
    this.profilePicture = claims?.ProfilePicturePath || '';
  }

  public routeToSettings(): void {
    this.router.navigateByUrl('/board/mainBoard/settings');
  }

  public LogoutUser(): void {
    sessionStorage.clear();
    this.router.navigateByUrl('auth');
  }
}
