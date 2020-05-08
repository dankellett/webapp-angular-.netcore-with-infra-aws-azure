import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  
  private http: HttpClient;
  private baseUrl: string;
  public users: UserDto[];

  constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string, private modalService: NgbModal) {
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

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      console.log(`Closed with: ${result}`);
    }, (reason) => {
      console.log(`Dismissed ${this.getDismissReason(reason)}`);
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}

class UserDto {
  userId: string;
  userName: string;
}
