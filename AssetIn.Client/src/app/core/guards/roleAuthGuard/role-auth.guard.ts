import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { JwtService } from '../../services/jwt/jwt.service';
import { inject } from '@angular/core';

export const roleAuthGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const router = inject(Router);
  const jwtService = inject(JwtService);
  const userJwt = sessionStorage.getItem('auth-jwt') || '';
  const userClaims = jwtService.getTokenClaims(userJwt);
  const userRoles = userClaims['Role'];
  const acceptableRoles = route.data['Roles'];
  for (let index = 0; index < acceptableRoles.length; index++) {
    if (userRoles.includes(acceptableRoles[index])) {
      return true;
    }
  }

  router.navigateByUrl('**');
  return false;
};
