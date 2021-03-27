import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { UserModel } from '../_models/usermodel';
import { AccountService } from '../_services/account.service'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}
  //loggedIn: boolean;
  //currentUser$: Observable<User>;


  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    //this.getCurrentUser();
    //this.currentUser$ = this.accountService.currnentUser$;
  }

  login() {
    console.log(this.model)
    this.accountService.login(this.model).subscribe(respose => {
      console.log(respose);
      this.router.navigateByUrl('cgstlist');
      //this.router.navigateByUrl('cgstadd');
      //this.loggedIn = true;
    }, error => {
      console.log(error);
      this.toastr.error(error);
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    //this.loggedIn = false;
  }

 
}
