import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { ComplaintDto, UserTypeEnum } from 'src/app/interfaces/DTOs';
import { ComplaintService } from 'src/app/services/complaint.service';
import { StoreManager } from 'src/app/shared/StoreManager';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit{
  isSubscribed: boolean = false;
  complaints: ComplaintDto[] = [];

constructor(private router: Router,
  private complaintService: ComplaintService,
  private messageService: MessageService,
  ) { }

ngOnInit() {
  this.loadComplaints();
}

get currentUserInfo() {
  return StoreManager.sessionStorageGetItem('userInfo')
}

get isAdminUser(): boolean {
  if(!!this.currentUserInfo)
      return this.currentUserInfo.UserDetails.UserType == UserTypeEnum.Administrator;

  return false;
}

loadComplaints() {
  if(this.isAdminUser) {
    this.complaintService
    .getallUsersComplaint()
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      this.complaints = data;
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
  else {
    this.complaintService
    .getUserComplaint(this.currentUserInfo.Id)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      this.complaints = data;
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
}

  logOut() {
    StoreManager.clearSessionStorageByKey('userInfo');
    this.router.navigate(['login']);
  }
}