import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { UserDto } from 'src/app/interfaces/DTOs';
import { AuthService } from 'src/app/services/auth.service';
import { StoreManager } from 'src/app/shared/StoreManager';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})

export class LoginComponent {
  isSubscribed: boolean = false;

  loginForm = this.fb.group({
    userName: ['', [Validators.required]],
    password: ['', Validators.required]
  })

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService,
    private msgService: MessageService,
  ) { }

  get userName() {
    return this.loginForm.controls['userName'];
  }

  get password() {
    return this.loginForm.controls['password'];
  }

  loginUser() {
    const { userName, password } = this.loginForm.value;

    this.authService
    .login(userName as string, password as string)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      if(!!data && data.Id != 0) {
        StoreManager.sessionStorageSetItem('userInfo', data);
        this.router.navigate(['/home']);
      }
      else
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Invalid User Info!!' });
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
}