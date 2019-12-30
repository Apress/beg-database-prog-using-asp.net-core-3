import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeesApiService } from '../employees-api/employees-api.service';
import { Employee } from '../models/employee';


@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
})


export class EmployeeListComponent implements OnInit {

  employees: Array<Employee> = [];
  message: string;
  employeesApi: EmployeesApiService;
  router: Router;


  constructor(employeesApi: EmployeesApiService, router: Router) {
    this.employeesApi = employeesApi;
    this.router = router;
  }

  ngOnInit() {

    if (!sessionStorage.hasOwnProperty("token")) {
      this.router.navigate(['/signin']);
    }

    this.employeesApi.selectAll().subscribe(
      data => this.employees = data as Array<Employee>,
      error => {
        if (error.status === 401) {
          this.router.navigate(['/signin']);
        }
        this.message = error.message
      }
    );

    if (sessionStorage.hasOwnProperty("message")) {
      this.message = sessionStorage.getItem("message");
      sessionStorage.removeItem("message");
    }
  }


  insert_click() {
    this.router.navigate(['/employees/insert']);
  }


  update_click(id) {
    this.router.navigate(['/employees/update', id]);
  }


  delete_click(id) {
    this.router.navigate(['/employees/delete', id]);
  }

}
