import { Component, inject, OnChanges, SimpleChanges } from '@angular/core';
import { nevbar } from '../../../core/constants/nevbar';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { JwtService } from '../../../core/services/jwt/jwt.service';
@Component({
  selector: 'app-nevbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './nevbar.component.html',
  styleUrl: './nevbar.component.css',
})
export class NevbarComponent implements OnChanges {
  private jwtServue: JwtService = inject(JwtService);
  private router: Router = inject(Router);
  public nevbarOptions: Array<any>;
  public currentRole: string;

  constructor() {
    let token = sessionStorage.getItem('auth-jwt') || '';
    this.currentRole = this.jwtServue.getTokenClaims(token)['Role'];
    this.nevbarOptions = this.initializeNevbarOptoins(nevbar);
  }
  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
  }

  private initializeNevbarOptoins(nevbar: Array<any>): Array<any> {
    let options = nevbar;
    options = options.filter(i => i.acceptableRoles.includes(this.currentRole));
    return options;
  }
}
