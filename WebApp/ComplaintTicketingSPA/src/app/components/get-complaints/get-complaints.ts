import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { takeWhile } from 'rxjs';
import { ComplaintDto, ComplaintStatus, DemandDto, UserTypeEnum } from 'src/app/interfaces/DTOs';
import { ComplaintService } from 'src/app/services/complaint.service';
import { StoreManager } from 'src/app/shared/StoreManager';

@Component({
  selector: 'get-complaints',
  templateUrl: './get-complaints.html'
})

export class GetComplaintsComponent implements OnInit{
  isSubscribed: boolean = false;
  complaints: ComplaintDto[] = [];
  DemandsItems: DemandDto[] = [];

  constructor(private router: Router,
    private complaintService: ComplaintService,
    private messageService: MessageService)
  { }

  ngOnInit(): void {
  this.loadComplaints();
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

  updateComplaintStatus(complaintId: number, status: ComplaintStatus) {
    this.complaintService
    .updateComplaintStatus(complaintId, status)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe(() => {
      this.messageService.add({ severity: 'error', summary: 'Success', detail: 'تمت العملية بنجاح' });
      this.loadComplaints();
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }

  get currentUserInfo() {
    return StoreManager.sessionStorageGetItem('userInfo')
  }
  
  get isAdminUser(): boolean {
    if(!!this.currentUserInfo)
        return this.currentUserInfo.UserDetails.UserType == UserTypeEnum.Administrator;
  
    return false;
  }

  deleteComplaint(complaintId: number) {
    this.complaintService
    .delete(complaintId)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
      this.loadComplaints();
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
  
  editComplaint(complaint: ComplaintDto) {
    this.complaintService
    .edit(complaint)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
      this.loadComplaints();
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
  
  deleteDemand(demandId: number) {
    this.DemandsItems.splice(demandId, 1);

    this.complaintService
    .deleteDemand(demandId)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
      this.loadComplaints();
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }
  
  editDemand(demand: DemandDto) {
    this.complaintService
    .updateDemand(demand)
    .pipe(takeWhile(() => this.isSubscribed, true),)
    .subscribe((data) => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
      this.loadComplaints();
    },
    exception => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: exception });
    });
  }

  addNewComplaint() {
    this.router.navigate(['Add']);
  }

  logOut() {
    StoreManager.clearSessionStorageByKey('userInfo');
    this.router.navigate(['login']);
  }
}
