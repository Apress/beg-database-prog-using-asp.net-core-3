import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from '../models/country';
import { EmployeesApiService } from '../employees-api/employees-api.service';
import { CountriesApiService } from '../countries-api/countries-api.service';


@Component({
  selector: 'app-employee-insert',
  templateUrl: './employee-insert.component.html'
})


export class EmployeeInsertComponent implements OnInit {

  formBuilder: FormBuilder;
  insertForm: FormGroup;
  router: Router;
  countries: Array<Country>;
  employeesApi: EmployeesApiService;
  countriesApi: CountriesApiService;
  message: string;


  constructor(formBuilder: FormBuilder,
    router: Router,
    employeeApi: EmployeesApiService,
    countriesApi: CountriesApiService) {

    this.formBuilder = formBuilder;
    this.router = router;
    this.employeesApi = employeeApi;
    this.countriesApi = countriesApi;

    this.insertForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.maxLength(10)]],
      lastName: ['', [Validators.required, Validators.maxLength(20)]],
      title: ['', [Validators.required, Validators.maxLength(30)]],
      birthDate: ['', [Validators.required]],
      hireDate: ['', [Validators.required]],
      country: ['', [Validators.required, Validators.maxLength(15)]],
      notes: ['', [Validators.maxLength(500)]]
    });
  }


  ngOnInit() {

    if (!sessionStorage.hasOwnProperty("token")) {
      this.router.navigate(["/signin"]);
    }


    this.countriesApi.selectAll()
      .subscribe(data => this.countries = data as Array<Country>
      , error => this.message = error.message);
  }



  save_click() {
    if (this.insertForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }
      this.employeesApi.insert(this.insertForm.value)
          .subscribe(() => this.message = "Employee inserted successfully!", error => {
              if (error.status === 401) {
                  this.router.navigate(["/signin"]);
              }
              this.message = error.message
          });
  }


  cancel_click() {
    this.router.navigate(["/employees/list"]);
  }

}
