import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

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
    private msgService: MessageService
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
    .subscribe((response) => {
        sessionStorage.setItem('loginSuccess', userName as string);
        this.router.navigate(['/home']);
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
}