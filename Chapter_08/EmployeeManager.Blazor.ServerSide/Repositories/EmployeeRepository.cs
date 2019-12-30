using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Blazor.ServerSide.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;


namespace EmployeeManager.Blazor.ServerSide.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private AppDbContext db = null;

        public EmployeeRepository(AppDbContext db)
        {
            this.db = db;
        }


        public List<Employee> SelectAll()
        {
            return db.Employees.ToList();
        }


        public Employee SelectByID(int id)
        {
            return db.Employees.Find(id);
        }


        public void Insert(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
        }

        public void Update(Employee emp)
        {
            db.Employees.Update(emp);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Employee emp = db.Employees.Find(id);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}
