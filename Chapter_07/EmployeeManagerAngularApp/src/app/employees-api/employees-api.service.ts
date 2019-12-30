import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Employee } from '../models/employee'; 


@Injectable()
export class EmployeesApiService {

  //baseUrl: string = 'http://localhost:5000/api/employees';
  baseUrl: string = '/api/employees';
  client: HttpClient;
  
  constructor(client: HttpClient) {
    this.client = client;
  }


  selectAll() {
    return this.client.get(this.baseUrl, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }


  selectByID(id: number) {
    return this.client.get(this.baseUrl + "/" + id, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      })
    });
  }


  insert(emp: Employee) {
    return this.client.post(this.baseUrl, emp, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token'),
        "Content-Type": "application/json"
      })
    });
  }


  update(emp: Employee) {
    return this.client.put(this.baseUrl + '/' + emp.employeeID, emp, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token'),
        "Content-Type": "application/json"
      })
    });
  }


  delete(id: number) {
    return this.client.delete(this.baseUrl + "/" + id, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + sessionStorage.getItem('token'),
        "Content-Type": "application/json"
      })
    });
  }

}
