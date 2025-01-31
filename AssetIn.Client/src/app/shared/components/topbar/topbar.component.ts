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
  @ViewChild('logoutDiv') private logoutDiv!: ElementRef;
  private router:Router=inject(Router)

  public toggleAvatarDiv():void {
    if (this.logoutDiv) {
      const divElement = this.logoutDiv.nativeElement;
      divElement.style.display =
        divElement.style.display === 'none' ? 'block' : 'none';
    }
  }
  public LogoutUser():void
  {
    debugger;
    sessionStorage.removeItem('auth-jwt');
    this.router.navigateByUrl('auth')
  }
  
}
