import { Component, inject } from '@angular/core';
import { nevbar } from '../../../core/constants/nevbar';
import { ActivatedRoute, RouterLink, RouterLinkActive } from '@angular/router';
import { JwtService } from '../../../core/services/jwt/jwt.service';
@Component({
  selector: 'app-nevbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './nevbar.component.html',
  styleUrl: './nevbar.component.css',
})
export class NevbarComponent {
  private jwtServue: JwtService = inject(JwtService);
  private activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  public nevbarOptions: Array<any>;
  public currentRole: string;

  constructor() {
    let token = sessionStorage.getItem('auth-jwt') || '';
    this.currentRole = this.jwtServue.getTokenClaims(token)['Role'];
    this.nevbarOptions = this.initializeNevbarOptoins(nevbar);
    console.log(this.activatedRoute.snapshot.url);
  }

  private initializeNevbarOptoins(nevbar: Array<any>): Array<any> {
    let options = nevbar;
    debugger;
    options = options.filter(i => i.acceptableRoles.includes(this.currentRole));
    return options;
  }
}
