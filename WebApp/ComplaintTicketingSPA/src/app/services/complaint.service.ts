import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User, UserExtendedDetails } from '../interfaces/auth';
import { Observable, catchError, map, throwError } from 'rxjs';

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

  Add(userDetails: User) {
    let url = this.baseUrl + "/AddComplaint";
    return this.http.post(url, userDetails).pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  UpdateUserInfo(userDetails: UserExtendedDetails) {
    return this.http.post(`${this.baseUrl}/UpdateUserInfo`, userDetails);
  }

  handleError(error: any) {
    let errMsg = error.error;
    console.error(errMsg); // log to console instead
    return throwError(errMsg);
}
}
