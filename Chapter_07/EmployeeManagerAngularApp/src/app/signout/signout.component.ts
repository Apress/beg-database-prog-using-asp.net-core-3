import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SecurityApiService } from '../security-api/security-api.service';


@Component({
  selector: 'app-signout',
  templateUrl: './signout.component.html'
})
export class SignOutComponent implements OnInit {

  userName: string;
  router: Router;

  constructor(router: Router) {
    this.router = router;
  }

  signout_click() {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('userName');
    this.router.navigate(["/signin"]);
  }

  ngOnInit() {
    this.userName = sessionStorage.getItem('userName');
  }

}
