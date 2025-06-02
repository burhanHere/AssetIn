import {
  Component,
  inject,
} from '@angular/core';
import { Router } from '@angular/router';
import { PageLoaderComponent } from '../page-loader/page-loader.component';
import { AlertCardComponent } from '../alert-card/alert-card.component';
import { RouteChangeDetectionService } from '../../../core/services/RouteChangeDetection/route-change-detection.service';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [PageLoaderComponent, AlertCardComponent],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.css',
})
export class TopbarComponent {
  private router: Router = inject(Router);
  private routeChangeDetectionService: RouteChangeDetectionService = inject(
    RouteChangeDetectionService
  );

  public showOverlay: boolean;
  public showSearchBar: boolean;
  public targetReport: string;
  private tableHeadersAndPropertyEmployee: any[];
  private tableHeadersAndPropertyAsset: any[];
  private tableHeadersAndPropertyAssetRequest: any[];
  public tableHeadersAndProperty: any[];
  public tableBody: any[];

  constructor() {
    this.showOverlay = false;
    this.showSearchBar = false;
    this.targetReport = '';
    this.tableHeadersAndPropertyEmployee = [
      { headerText: 'Id', propertyName: 'Id' },
      { headerText: 'Full Name', propertyName: 'FullName' },
      { headerText: 'User Name', propertyName: 'UserName' },
      { headerText: 'Email', propertyName: 'Email' },
      { headerText: 'Phone Number', propertyName: 'PhoneNumber' }
    ];
    this.tableHeadersAndPropertyAsset = [];
    this.tableHeadersAndPropertyAssetRequest = [];
    this.tableHeadersAndProperty = [];
    this.tableBody = [];
    this.routeChangeDetectionService.routeChanged.subscribe(() => {
      this.showSearchBar = this.router.url.includes(
        '/board/mainBoard/organizationAdmin'
      );
    });
  }

  public getTargetData(event: Event): void {
    this.targetReport = (event.target as HTMLSelectElement).value;
    if (this.targetReport === 'Assets') {
      this.getAssets()
    } else if (this.targetReport === 'Employee') {
      this.getEmployees()
    } else if (this.targetReport === 'Assets Request') {
      this.getAssetRequest();
    }
  }

  private getEmployees(): void {

  }

  private getAssetRequest(): void {

  }

  private getAssets(): void {

  }


  public LogoutUser(): void {
    sessionStorage.removeItem('auth-jwt');
    sessionStorage.removeItem('targetOrganizationID');
    this.router.navigateByUrl('auth');
  }
}
