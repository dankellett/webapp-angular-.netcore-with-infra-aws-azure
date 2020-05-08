import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { UserDto } from 'src/app/shared/models/user.dto';

@Injectable({
  providedIn: 'root'
})
export class UserdataService {

  private http: HttpClient;
  private baseUrl: string;

  constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string) { 
    this.http = _http;
    this.baseUrl = _baseUrl;
  }

  public getUsers(){
    return this.http.get<UserDto[]>(this.baseUrl + 'users');
    //.subscribe(result => { }, error => console.error(error));
  }

  public updateUser(userDto: UserDto){
    return this.http.post(this.baseUrl + 'user', userDto);
  }
}
