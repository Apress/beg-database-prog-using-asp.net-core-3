import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SecurityApiService } from '../security-api/security-api.service';


@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html'
})
export class SignInComponent implements OnInit {

  formBuilder: FormBuilder;
  route: ActivatedRoute;
  router: Router;
  securityApi: SecurityApiService;
  signinForm: FormGroup;
  message: string;


  constructor(formBuilder: FormBuilder,
    route: ActivatedRoute,
    router: Router,
    securityService: SecurityApiService) {


    this.formBuilder = formBuilder;
    this.route = route;
    this.router = router;
    this.securityApi = securityService;


    this.signinForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.maxLength(20)]],
      password: ['', [Validators.required, Validators.maxLength(20)]]
    });
  }



  signin_click() {
    if (this.signinForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }
    this.securityApi.signIn(this.signinForm.value)
      .subscribe(token => {
        sessionStorage.setItem('token', token.token);
        sessionStorage.setItem('userName', this.signinForm.controls['userName'].value);
        this.router.navigate(["/employees/list"])
      }, error => this.message = "Unable to Sign-in");
  }



  ngOnInit() {
  }


}
