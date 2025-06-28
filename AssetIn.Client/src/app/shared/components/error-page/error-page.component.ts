import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-error-page',
  standalone: true,
  imports: [],
  templateUrl: './error-page.component.html',
  styleUrl: './error-page.component.css'
})
export class ErrorPageComponent {
  private router: Router = inject(Router);
  goBack(): void {
    sessionStorage.clear();
    this.router.navigateByUrl('');
  }
}
