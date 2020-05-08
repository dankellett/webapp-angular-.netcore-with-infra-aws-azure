import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { UserDto } from 'src/app/shared/models/user.dto';
import { UserdataService } from 'src/app/core/services/userdata.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  
  public users: UserDto[];
  private userDataService: UserdataService;
  private modalUserObject: UserDto;

  constructor(private modalService: NgbModal, private _userDataService: UserdataService) {
    this.userDataService = _userDataService;
  }

  ngOnInit() {
    this.getUsers();
  }

  openEditUserModal(content, user) {
    this.modalUserObject = Object.assign({}, user);
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      console.log(`Closed with: ${result}`);
    }, (reason) => {
      console.log(`Dismissed ${this.getDismissReason(reason)}`);
    });
  }

  saveUser(){
    this.userDataService.updateUser(this.modalUserObject).subscribe(result => {
      console.log(result);
    })
  }

  private getUsers(){
    this.userDataService.getUsers().subscribe(result => {
      this.users = result;
    }, error => console.error(error));
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
