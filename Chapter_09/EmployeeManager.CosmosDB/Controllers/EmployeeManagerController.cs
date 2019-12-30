using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.CosmosDB.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;



namespace EmployeeManager.CosmosDB.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {

        private DocumentClient client;
        private Uri employeeCollectionUri;
        private Uri countryCollectionUri;
        private string databaseName;
        private string employeeCollectionName;
        private string countryCollectionName;


        public EmployeeManagerController(IConfiguration config)
        {
            var uri = new Uri(config.GetValue<string>("CosmosDBSettings:Server"));
            var primaryKey = config.GetValue<string>("CosmosDBSettings:PrimaryKey");
            databaseName = config.GetValue<string>("CosmosDBSettings:DatabaseName");
            employeeCollectionName = config.GetValue<string>("CosmosDBSettings:EmployeeCollectionName");
            countryCollectionName = config.GetValue<string>("CosmosDBSettings:CountryCollectionName");

            client = new DocumentClient(uri,primaryKey);

            this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName }).Wait();

            this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = employeeCollectionName }).Wait();

            this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = countryCollectionName }).Wait();


            this.employeeCollectionUri = UriFactory
                .CreateDocumentCollectionUri(
                    databaseName, employeeCollectionName);

            this.countryCollectionUri = UriFactory
                .CreateDocumentCollectionUri(
                    databaseName, countryCollectionName);

            
        }


        public void FillCountries()
        {
            if (client.CreateDocumentQuery<Country>(countryCollectionUri).Count() == 0)
            {
                Country usa = new Country() { DocumentID = Guid.NewGuid(), CountryID = 1, Name = "USA" };
                Country uk = new Country() { DocumentID = Guid.NewGuid(), CountryID = 2, Name = "UK" };

                client.CreateDocumentAsync(countryCollectionUri, usa).Wait();
                client.CreateDocumentAsync(countryCollectionUri, uk).Wait();
            }

            var ctry = client.CreateDocumentQuery<Country>(countryCollectionUri).ToList();

            List<SelectListItem> countries = (from c in ctry select new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();

            ViewBag.Countries = countries;
        }


        public IActionResult List()
        {
            var model = client.CreateDocumentQuery<Employee>(employeeCollectionUri).OrderBy(e=>e.EmployeeID).ToList();

            return View(model);
        }


        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(Employee emp)
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                Employee obj = client.CreateDocumentQuery<Employee>(employeeCollectionUri).Where(e => e.EmployeeID == emp.EmployeeID).AsEnumerable().SingleOrDefault();

                if (obj == null)
                {
                    emp.DocumentID = Guid.NewGuid();
                    await client.CreateDocumentAsync(employeeCollectionUri, emp);
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

            Guid docId = new Guid(id);

            Employee emp = client.CreateDocumentQuery<Employee>(employeeCollectionUri).Where(e => e.DocumentID == docId).AsEnumerable().SingleOrDefault();

            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Employee emp)
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, employeeCollectionName, emp.DocumentID.ToString()), emp);

                ViewBag.Message = "Employee updated successfully!";
            }
            return View(emp);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {
            Guid docId = new Guid(id);
            Employee emp = client.CreateDocumentQuery<Employee>(employeeCollectionUri).Where(e => e.DocumentID == docId).AsEnumerable().SingleOrDefault();
            return View(emp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string documentID)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseName, employeeCollectionName, documentID));

            TempData["Message"] = "Employee deleted successfully";

            return RedirectToAction("List");
        }


    }
}
