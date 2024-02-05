import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User, UserExtendedDetails } from '../interfaces/auth';
import { Observable, catchError, map, throwError } from 'rxjs';
import { ComplaintDto, ComplaintStatus } from '../interfaces/DTOs';

@Injectable({
  providedIn: 'root'
})
export class ComplaintService {

  private baseUrl = 'http://localhost:5225/api/ComplaintTicketing';

  constructor(private http: HttpClient) { }

  getallUsersComplaint(): Observable<any> {
    let url = this.baseUrl + "/GetUsersComplaints";
    return this.http.get(url).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  getUserComplaint(userId: number): Observable<any> {
    let url = this.baseUrl + "/GetByUserId?userId=" + userId;
    return this.http.get(url).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  getComplaintById(complaintId: number): Observable<any> {
    let url = this.baseUrl + "/GetComplaintById?complaintId=" + complaintId;
    return this.http.get(url).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  add(complaint: ComplaintDto) {
    let url = this.baseUrl + "/AddComplaint";
    return this.http.post(url, complaint).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  edit(complaint: ComplaintDto) {
    let url = this.baseUrl + "/EditComplaint";
    return this.http.post(url, complaint).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  delete(complaintId: number) {
    let url = this.baseUrl + "/DeleteComplaint";
    return this.http.post(url, complaintId).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  deleteDemand(demandId: number) {
    let url = this.baseUrl + "/DeleteDemand";
    return this.http.post(url, demandId).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  updateComplaintStatus(complaintId: number, status: ComplaintStatus) {
    const url = `${this.baseUrl}/UpdateComplaintStatus?complaintId=${complaintId}&status=${status}`;
    return this.http.post(url, {}).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  handleError(error: any) {
    let errMsg = error.error;
    console.error(errMsg); // log to console instead
    return throwError(errMsg);
}
}
