import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { ApiResponse } from '../../../core/models/apiResponse';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrl: './email-confirmation.component.css',
})
export class EmailConfirmationComponent implements OnInit {
  private activeRoute: ActivatedRoute = inject(ActivatedRoute);
  private authenticationService: AuthenticationService = inject(
    AuthenticationService
  );
  private router: Router = inject(Router);
  private token: string = '';
  private email: string = '';
  public isLoading: boolean = true;
  public showAlert: boolean = false;
  public alertCardTitle: string = '';
  public alertCardMessage: string = '';
  ngOnInit(): void {
    this.activeRoute.queryParams.subscribe((params) => {
      this.token = encodeURIComponent(params['token']);
      this.email = encodeURIComponent(params['email']);
    });
    this.authenticationService.ConfirmEmail(this.token, this.email).subscribe(
      (response: ApiResponse) => {
        this.alertCardTitle = 'SUCCESS🎉';
        this.alertCardMessage = response.responseData[0];
        this.isLoading = false;
        this.showAlert = true;
      },
      (error: HttpErrorResponse) => {
        //404not found-> user not found
        //400bad request-> failed to confirm email
        //403conflict->email already confirmed
        if (error.status == 409 || error.status == 400) {
          this.alertCardTitle = error.status.toString();
          this.alertCardMessage = error.error.responseData[0];
        } else if (error.status == 404) {
          this.alertCardTitle = 'Error';
          this.alertCardMessage = 'Some error occured';
        } else {
          this.alertCardTitle = 'Error';
          this.alertCardMessage =
            'Some error occured֫. Try again by getting a new link.';
        }
        this.isLoading = false;
        this.showAlert = true;
      }
    );
  }

  public dismissAlertAndLogin(event: boolean): void {
    this.showAlert = event;
    this.router.navigateByUrl('auth/signIn');
  }
}
