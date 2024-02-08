import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { ComplaintDto, DemandDto, UserTypeEnum } from 'src/app/interfaces/DTOs';
import { ComplaintService } from 'src/app/services/complaint.service';
import { StoreManager } from 'src/app/shared/StoreManager';

@Component({
  selector: 'add-new-complaint',
  templateUrl: './add-new-complaint.html'
})
export class AddNewComplaintComponent implements OnInit{
  isSubscribed: boolean = false;
  complaints: ComplaintDto[] = [];

constructor(private router: Router,
  private complaintService: ComplaintService,
  private messageService: MessageService)
  { }

ngOnInit() {
}

get currentUserInfo() {
  return StoreManager.sessionStorageGetItem('userInfo')
}

get isAdminUser(): boolean {
  if(!!this.currentUserInfo)
      return this.currentUserInfo.UserDetails.UserType == UserTypeEnum.Administrator;

  return false;
}

newComplaintTextEn: string = '';
newComplaintTextAr: string = '';

get disableComplaintAdd() {
  return !this.newComplaintTextEn || this.newComplaintTextEn.trim() == '' ||
  !this.newComplaintTextAr || this.newComplaintTextAr.trim() == '';
}

ComplaintItem: ComplaintDto = new ComplaintDto();
AddNewComplaintRecord() {
  this.ComplaintItem = new ComplaintDto();
  this.ComplaintItem.ComplaintTextEn = this.newComplaintTextEn;
  this.ComplaintItem.ComplaintTextAr = this.newComplaintTextAr;

  if(!!this.newDemandTextAr && this.newDemandTextAr.trim() != '' &&
     !!this.newDemandTextEn && this.newDemandTextEn.trim() != '') {
      let DemandsItem = new DemandDto();
      DemandsItem.DemandTextEn = this.newDemandTextEn;
      DemandsItem.DemandTextAr = this.newDemandTextAr;
    
      this.DemandsItems.push(DemandsItem);
     }

  this.ComplaintItem.Demands = this.DemandsItems;

  this.complaintService
  .add(this.ComplaintItem)
  .pipe(takeWhile(() => this.isSubscribed, true),)
  .subscribe((data) => {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Added successfully' });
  },
  exception => {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
  });

  this.DemandsItems = [];
  this.newComplaintTextEn = '';
  this.newComplaintTextAr = '';
  this.newDemandTextAr = '';
  this.newDemandTextEn = '';
}

get disableDemandAdd() {
  return !this.newDemandTextEn || this.newDemandTextEn.trim() == '' ||
  !this.newDemandTextAr || this.newDemandTextAr.trim() == '';
}

get siderBarTitle() {
  return this.isAdminUser ? 'Users Complaints' : 'My Complaints'
}

DemandsItems: DemandDto[] = [];
newDemandTextEn: string = ''
newDemandTextAr: string = ''
AddNewDemandRecord() {
  let DemandsItem = new DemandDto();
  DemandsItem.DemandTextEn = this.newDemandTextEn;
  DemandsItem.DemandTextAr = this.newDemandTextAr;

  this.DemandsItems.push(DemandsItem);

  this.newDemandTextEn = '';
  this.newDemandTextAr = '';
}

getMyComplaints() {
  this.router.navigate(['View']);
}

  logOut() {
    StoreManager.clearSessionStorageByKey('userInfo');
    this.router.navigate(['login']);
  }
}