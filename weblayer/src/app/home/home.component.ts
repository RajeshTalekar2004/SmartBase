import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode
  }

  getUsers() {
    this.http.get<ServiceResponseModel>('http://localhost:9000/api/v1/User/GetAll').subscribe(response => {
      this.users = response.data;
    }, error => {
      console.log(error);
    })
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}

interface ServiceResponseModel {
  data: Array<object>;
  success: boolean;
  message: string;
}
