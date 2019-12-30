import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeInsertComponent } from './employee-insert/employee-insert.component';
import { EmployeeUpdateComponent } from './employee-update/employee-update.component';
import { EmployeeDeleteComponent } from './employee-delete/employee-delete.component';
import { SignInComponent } from './signin/signin.component';
import { RegisterComponent } from './register/register.component';




const routes: Routes = [
  { path: "", redirectTo: "employees/list", pathMatch: 'full' },
  { path: "register", component: RegisterComponent },
  { path: "signin", component: SignInComponent },
  { path: "employees/list", component: EmployeeListComponent },
  { path: "employees/insert", component: EmployeeInsertComponent },
  { path: "employees/update/:id", component: EmployeeUpdateComponent },
  { path: "employees/delete/:id", component: EmployeeDeleteComponent },
  { path: '**', component: EmployeeListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
