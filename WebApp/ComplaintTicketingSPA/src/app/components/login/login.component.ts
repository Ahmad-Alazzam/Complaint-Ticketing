import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { StoreManager } from 'src/app/shared/StoreManager';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})

export class LoginComponent implements OnInit {
  isSubscribed: boolean = false;
  
  userName: string = '';
  password: string = '';
  loginForm: FormGroup = this.fb.group({
    userName: [this.userName, [Validators.required]],
    password: [this.password, Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService,
  ) { }

  ngOnInit(): void { }

  loginUser() {

    if(this.loginForm.valid) {
      const userName = this.loginForm.get('userName')?.value;
      const password = this.loginForm.get('password')?.value;
   
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
    else {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Invalid Login Info!' });
      return;
    }
  }
}
