import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  
  private http: HttpClient;
  private baseUrl: string;
  public users: UserDto[];

  constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string) {
    this.http = _http;
    this.baseUrl = _baseUrl;
  }

  ngOnInit() {
    this.getUsers();
  }

  private getUsers(){
    this.http.get<UserDto[]>(this.baseUrl + 'users').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }
}

class UserDto {
  userId: string;
  userName: string;
}
