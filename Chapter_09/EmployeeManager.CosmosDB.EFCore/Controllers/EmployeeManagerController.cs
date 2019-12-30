using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.CosmosDB.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Cosmos;



namespace EmployeeManager.CosmosDB.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {

        private readonly AppDbContext db;

        public EmployeeManagerController(AppDbContext db)
        {
            this.db = db;
            db.Database.EnsureCreated();
        }


        public void FillCountries()
        {
            if (db.Countries.AsEnumerable().Count() == 0)
            {
                Country usa = new Country() { DocumentID = Guid.NewGuid(), CountryID = 1, Name = "USA" };
                Country uk = new Country() { DocumentID = Guid.NewGuid(), CountryID = 2, Name = "UK" };
                db.Countries.Add(usa);
                db.Countries.Add(uk);
                db.SaveChanges();
            }

            List<SelectListItem> countries = (from c in db.Countries select new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();

            ViewBag.Countries = countries;
        }


        public IActionResult List()
        {
            var query = from e in db.Employees
                        orderby e.EmployeeID
                        select e;

            return View(query.ToList());
        }


        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Employee emp)
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                Employee obj = (from e in db.Employees
                                where e.EmployeeID == emp.EmployeeID
                                select e).SingleOrDefault();

                if (obj == null)
                {
                    emp.DocumentID = Guid.NewGuid();
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    ViewBag.Message = "Employee inserted successfully!";
                }
                else
                {
                    ViewBag.Message = "EmployeeID already exists!";
                }
            }
            return View(emp);
        }


        public IActionResult Update(string id)
        {
            FillCountries();

            Employee emp = db.Employees.Find(new Guid(id));

            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee emp)
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                Employee obj = db.Employees.Find(emp.DocumentID);
                obj.EmployeeID = emp.EmployeeID;
                obj.FirstName = emp.FirstName;
                obj.LastName = emp.LastName;
                obj.Title = emp.Title;
                obj.BirthDate = emp.BirthDate;
                obj.HireDate = emp.HireDate;
                obj.Country = emp.Country;
                obj.Notes = emp.Notes;
                db.SaveChanges();
                ViewBag.Message = "Employee updated successfully!";
            }
            return View(emp);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {
            Employee emp = db.Employees.Find(new Guid(id));
            return View(emp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string documentID)
        {
            Employee emp = db.Employees.Find(new Guid(documentID));

            db.Employees.Remove(emp);
            db.SaveChanges();

            TempData["Message"] = "Employee deleted successfully!";

            return RedirectToAction("List");
        }


    }
}
