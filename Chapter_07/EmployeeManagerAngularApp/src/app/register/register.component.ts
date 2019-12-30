import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SecurityApiService } from '../security-api/security-api.service';



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  formBuilder: FormBuilder;
  route: ActivatedRoute;
  router: Router;
  securityApi: SecurityApiService;
  registerForm: FormGroup;
  message: string;


  constructor(formBuilder: FormBuilder,
    route: ActivatedRoute,
    router: Router,
    securityApi: SecurityApiService) {


    this.formBuilder = formBuilder;
    this.route = route;
    this.router = router;
    this.securityApi = securityApi;


    this.registerForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.maxLength(20)]],
      password: ['', [Validators.required, Validators.maxLength(20)]],
      confirmPassword: ['', [Validators.required, Validators.maxLength(20)]],
      email: ['', [Validators.required, Validators.maxLength(100),Validators.email]],
      fullName: ['', [Validators.required, Validators.maxLength(100)]],
      birthDate: ['', [Validators.required]]
    });
  }



  create_click() {
    if (this.registerForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }
    this.securityApi.register(this.registerForm.value)
      .subscribe(msg => {
        this.router.navigate(["/signin"])
      }, error => this.message = error.error);
  }


 
  ngOnInit() {
  }

}
