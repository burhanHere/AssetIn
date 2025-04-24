import { HttpInterceptorFn } from '@angular/common/http';

export const authenticationInterceptor: HttpInterceptorFn = (req, next) => {
  // Your interceptor logic here
  const userJwt = sessionStorage.getItem('auth-jwt');
  if (userJwt) {
    const reqClone = req.clone({
      setHeaders: {
        Authorization: `Bearer ${userJwt}`,
      },
    });
    return next(reqClone);
  }
  return next(req);
};
