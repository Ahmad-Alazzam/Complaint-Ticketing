import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AddNewComplaintComponent } from './components/add-new-complaint/add-new-complaint';
import { authGuard } from './guards/auth.guard';
import { GetComplaintsComponent } from './components/get-complaints/get-complaints';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'Add',
    component: AddNewComplaintComponent,
    canActivate: [authGuard]
  },
  {
    path: 'View',
    component: GetComplaintsComponent,
    canActivate: [authGuard]
  },
  {
    path: '', redirectTo: 'Add', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
