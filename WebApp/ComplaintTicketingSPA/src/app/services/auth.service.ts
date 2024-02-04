import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User, UserExtendedDetails } from '../interfaces/auth';
import { Observable, catchError, map, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:5225/api/User';

  constructor(private http: HttpClient) { }

  login(userName: string, password: string): Observable<any> {
    let url = this.baseUrl + "/Login?userName=" + userName + "&password=" + password;
    return this.http.post(url, "").pipe(
      map(res => <any>res),
      catchError(this.handleError));
  }

  registerUser(userDetails: User) {
    let url = this.baseUrl + "/Register";
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
