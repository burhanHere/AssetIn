import { Component, ElementRef, inject, Inject, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.css',
})
export class TopbarComponent {
  private router: Router = inject(Router)

  public LogoutUser(): void {
    sessionStorage.removeItem('auth-jwt');
    sessionStorage.removeItem('targetOrganizationID');
    this.router.navigateByUrl('auth')
  }

}
