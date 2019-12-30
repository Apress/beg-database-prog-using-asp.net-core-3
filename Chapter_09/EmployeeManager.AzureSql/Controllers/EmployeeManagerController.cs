using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.AzureSql.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeManager.AzureSql.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManager.AzureSql.Repositories;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.AzureSql.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {
        private IEmployeeRepository employeeRepository;

        private ICountryRepository countryRepository;

        public EmployeeManagerController(IEmployeeRepository empRepository, ICountryRepository ctryRepository)
        {
            this.employeeRepository = empRepository;
            this.countryRepository = ctryRepository;
        }


        private void FillCountries()
        {
            List<Country> countriesList = countryRepository.SelectAll();

            List<SelectListItem> countries = (from c in countriesList
                                              orderby c.Name ascending
                                              select new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();

            ViewBag.Countries = countries;
        }

        
        public IActionResult List()
        {
            List<Employee> model = employeeRepository.SelectAll();
            return View(model);
        }

        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                employeeRepository.Insert(model);
                ViewBag.Message = "Employee inserted successfully!";
            }
            return View(model);
        }

        public IActionResult Update(int id)
        {
            FillCountries();
            Employee model = employeeRepository.SelectByID(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee model)
        {
            FillCountries();

            if(ModelState.IsValid)
            {
                employeeRepository.Update(model);
                ViewBag.Message = "Employee updated successfully!";
            }
            return View(model);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            Employee model = employeeRepository.SelectByID(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int employeeID)
        {
            employeeRepository.Delete(employeeID);
            TempData["Message"] = "Employee deleted successfully!";
            return RedirectToAction("List");
        }
    }
}
