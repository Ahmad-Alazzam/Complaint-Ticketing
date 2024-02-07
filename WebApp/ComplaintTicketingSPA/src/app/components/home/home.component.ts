import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { ComplaintDto, DemandDto, UserTypeEnum } from 'src/app/interfaces/DTOs';
import { ComplaintService } from 'src/app/services/complaint.service';
import { StoreManager } from 'src/app/shared/StoreManager';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./style.css']
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

newComplaintText: string = '';
get disableComplaintAdd() {
  return !this.newComplaintText || !this.newComplaintText.trim();
}

ComplaintItem: ComplaintDto = new ComplaintDto();
AddNewComplaintRecord() {

  this.ComplaintItem = new ComplaintDto();
  this.ComplaintItem.ComplaintTextAr = this.newComplaintText;
  this.ComplaintItem.Demands = this.DemandsItems;

  this.complaintService
  .add(this.ComplaintItem)
  .pipe(takeWhile(() => this.isSubscribed, true),)
  .subscribe((data) => {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
  },
  exception => {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
  });

  this.newComplaintText = '';
}

get disableDemandAdd() {
  return !this.newDemandText || !this.newDemandText.trim();
}

DemandsItems: DemandDto[] = [];
newDemandText: string = ''
AddNewDemandRecord() {

  let DemandsItem = new DemandDto();
  DemandsItem.DemandTextAr = this.newDemandText;

  this.DemandsItems.push(DemandsItem);

  this.newDemandText = '';
}

deleteComplaint(complaintId: number) {
  this.complaintService
  .delete(complaintId)
  .pipe(takeWhile(() => this.isSubscribed, true),)
  .subscribe((data) => {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
  },
  exception => {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
  });
}

editComplaint(complaint: ComplaintDto) {

}

deleteDemand(demandId: number) {
  this.DemandsItems.splice(demandId, 1);
}

editDemand(demand: DemandDto) {

}

  logOut() {
    StoreManager.clearSessionStorageByKey('userInfo');
    this.router.navigate(['login']);
  }
}