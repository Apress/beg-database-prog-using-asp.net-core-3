import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeesApiService } from '../employees-api/employees-api.service';
import { Employee } from '../models/employee';


@Component({
  selector: 'app-employee-delete',
  templateUrl: './employee-delete.component.html'
})


export class EmployeeDeleteComponent implements OnInit {

  formBuilder: FormBuilder;
  route: ActivatedRoute;
  router: Router;
  employeesApi: EmployeesApiService;
  deleteForm: FormGroup;
  id: number;
  message: string;
  employee: Employee;
  

  constructor(formBuilder: FormBuilder,
    route: ActivatedRoute,
    router: Router,
    employeesApi: EmployeesApiService) {

    this.formBuilder = formBuilder;
    this.route = route;
    this.router = router;
    this.employeesApi = employeesApi;


    if (this.route.snapshot.params["id"]) {
      this.id = this.route.snapshot.params["id"];
    }


    this.deleteForm = this.formBuilder.group({
      employeeID: ['', [Validators.required]]
    });

  }


  ngOnInit() {

    if (!sessionStorage.hasOwnProperty("token")) {
      this.router.navigate(["/signin"]);
    }


    this.employeesApi.selectByID(this.id)
      .subscribe(data => {
        this.employee = data as Employee;
        this.deleteForm.controls['employeeID'].setValue(this.employee.employeeID);
      }
        , error => {
          if (error.status === 401) {
            this.router.navigate(["/signin"]);
          }
          this.message = error.message
        });
  }



  delete_click() {
    if (this.deleteForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }
      this.employeesApi.delete(this.deleteForm.controls["employeeID"].value)
          .subscribe(() => {
              sessionStorage.setItem("message", "Employee deleted successfully!"
              );
              this.router.navigate(['/employees/list']);
          }, error => {
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
