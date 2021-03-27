import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { UserModel } from '../_models/usermodel';
import { ServiceResponseModel } from '../_models/serviceresponseModel';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;

  private currnentUserSource = new ReplaySubject<UserModel>(1);
  currnentUser$ = this.currnentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'User/Login', model).pipe(
      map((response: ServiceResponseModel) => {
        let jsonObj: any = JSON.stringify(response.data);
        let user: UserModel = <UserModel>jsonObj;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currnentUserSource.next(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'User/Add', model).pipe(
      map((response: ServiceResponseModel) => {
        let jsonObj: any = JSON.stringify(response.data);
        let user: UserModel = <UserModel>jsonObj;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currnentUserSource.next(user);
        }
        return user;
      })
    )
  }

  setCurrentUser(user: UserModel) {
    this.currnentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currnentUserSource.next(null);
  }

}