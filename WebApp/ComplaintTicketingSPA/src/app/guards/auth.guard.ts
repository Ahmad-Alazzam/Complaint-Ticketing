import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { StoreManager } from '../shared/StoreManager';

export const authGuard: CanActivateFn = (route, state) => {
  let userInfo = StoreManager.sessionStorageGetItem('userInfo')
  if (!!userInfo.Id) {
    return true;
  } else {
    const router = inject(Router);
    return router.navigate(['login']);
  }
};
