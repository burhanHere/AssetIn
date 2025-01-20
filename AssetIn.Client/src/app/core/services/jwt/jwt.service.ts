import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
@Injectable({
  providedIn: 'root',
})
export class JwtService {
  constructor() {}

  public getTokenClaims(token:string): any {
    if (token === null || token === undefined || token === '') {
      return {};
    }
    const tokenPayload = jwtDecode(token);
    return tokenPayload;
  }
}
