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

  public showSearchBar: boolean = false;
  constructor() {
    this.routeChangeDetectionService.routeChanged.subscribe(() => {
      this.showSearchBar = this.router.url.includes(
        '/board/mainBoard/organizationAdmin'
      );
    });
  }

  public LogoutUser(): void {
    sessionStorage.removeItem('auth-jwt');
    sessionStorage.removeItem('targetOrganizationID');
    this.router.navigateByUrl('auth');
  }
}
