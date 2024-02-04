import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { User } from 'src/app/interfaces/auth';
import { AuthService } from 'src/app/services/auth.service';
import { passwordMatchValidator } from 'src/app/shared/password-match.directive';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  isSubscribed: boolean = false;

  registerForm = this.fb.group({
    UserName: ['', [Validators.required]],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    UserDetails: this.fb.group({
      Name: ['', [Validators.required, Validators.pattern(/^[a-zA-Z]+(?: [a-zA-Z]+)*$/)]],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNumber: ['', []],
      DateOfBirth: ['', Validators.required]
    })
  }, {
    validators: passwordMatchValidator
  });

  getFormControl(controlPath: string): FormControl<string> {
    return this.registerForm.get(controlPath) as FormControl<string>;
  }
  
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private messageService: MessageService,
    private router: Router
  ) { }

  get userName() {
    return this.registerForm.controls['UserName'];
  }

  get password() {
    return this.registerForm.controls['Password'];
  }

  get confirmPassword() {
    return this.registerForm.controls['ConfirmPassword'];
  }

  get phoneNumber() {
    return this.registerForm.controls['Password'];
  }

  get fullName() {
    return this.registerForm.get('UserDetails.Name');
  }

  get dateOfBirth() {
    return this.registerForm.get('UserDetails.DateOfBirth');
  }

  get email() {
    return this.registerForm.get('UserDetails.Email');
  }

  submitDetails() {
    const postData = { ...this.registerForm.value };
    delete postData.ConfirmPassword;    

    this.authService
    .registerUser(postData as User)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((response) => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Register successfully' });
      this.router.navigate(['login'])
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
}