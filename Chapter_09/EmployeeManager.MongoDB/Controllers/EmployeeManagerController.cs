using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.MongoDB.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.MongoDB.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {

        private IMongoCollection<Employee> employees;

        private IMongoCollection<Country> countries;

        public EmployeeManagerController(IConfiguration config)
        {
            var client = new MongoClient(config.GetValue<string>("MongoDBSettings:Server"));
            IMongoDatabase db = client.GetDatabase(config.GetValue<string>("MongoDBSettings:DatabaseName"));
            this.employees = db.GetCollection<Employee>(config.GetValue<string>("MongoDBSettings:EmployeeCollectionName"));
            this.countries = db.GetCollection<Country>(config.GetValue<string>("MongoDBSettings:CountryCollectionName"));
        }


        public void FillCountries()
        {

            if (this.countries.CountDocuments(FilterDefinition<Country>.Empty) == 0)
            {
                Country usa = new Country() { DocumentID = new ObjectId(), CountryID = 1, Name = "USA" };
                Country uk = new Country() { DocumentID = new ObjectId(), CountryID = 1, Name = "UK" };

                this.countries.InsertOne(usa);
                this.countries.InsertOne(uk);
            }

            var ctry = this.countries.Find(FilterDefinition<Country>.Empty).ToList();

            List<SelectListItem> countries = (from c in ctry
                                              select new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();

            ViewBag.Countries = countries;
        }


        public IActionResult List()
        {
            var model = this.employees.Find(FilterDefinition<Employee>.Empty).ToList();

            return View(model);
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
                Employee existing = this.employees.Find(e => e.EmployeeID == emp.EmployeeID).FirstOrDefault();

                if (existing == null)
                {
                    this.employees.InsertOne(emp);
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

            ObjectId docID = new ObjectId(id);
            Employee emp = this.employees.Find(e => e.DocumentID == docID).FirstOrDefault();
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string documentID, Employee emp)
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                emp.DocumentID = new ObjectId(documentID);
                var filter = Builders<Employee>.Filter.Eq(e => e.DocumentID, emp.DocumentID);
                var result = employees.ReplaceOne(filter, emp);

                if (result.IsAcknowledged)
                {
                    ViewBag.Message = "Employee updated successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while updating Employee!";
                }
            }
            return View(emp);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {
            ObjectId docID = new ObjectId(id);
            Employee emp = this.employees.Find(e => e.DocumentID == docID).FirstOrDefault();
            return View(emp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string documentID)
        {
            ObjectId docID = new ObjectId(documentID);
            var result = this.employees.DeleteOne<Employee>(e => e.DocumentID == docID);

            if (result.IsAcknowledged)
            {
                TempData["Message"] = "Employee deleted successfully!";
            }
            else
            {
                TempData["Message"] = "Error while deleting Employee!";
            }
            return RedirectToAction("List");
        }


    }
}
