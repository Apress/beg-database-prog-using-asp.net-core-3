using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.RazorPages.Pages
{
    [Authorize(Roles = "Manager")]
    public class UpdateModel : PageModel
    {
        private readonly AppDbContext db = null;

        [BindProperty]
        public Employee Employee { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public string Message { get; set; }

        public bool DataFound { get; set; } = true;

        public UpdateModel(AppDbContext db)
        {
            this.db = db;
        }


        public void FillCountries()
        {
            List<SelectListItem> countries = (from c in db.Countries select new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();

            this.Countries = countries;
        }

        public void OnGet(int id)
        {
            FillCountries();

            Employee = db.Employees.Find(id);

            if (Employee == null)
            {
                this.DataFound = false;
                this.Message = "EmployeeID Not Found.";
            }
            else
            {
                this.DataFound = true;
                this.Message = "";
            }
        }


        public void OnPost()
        {
            FillCountries();

            if (ModelState.IsValid)
            {
                try
                {
                    db.Employees.Update(Employee);
                    db.SaveChanges();
                    Message = "Employee updated successfully!";
                }
                catch (DbUpdateException ex1)
                {
                    Message = ex1.Message;
                    if (ex1.InnerException != null)
                    {
                        Message += " : " + ex1.InnerException.Message;
                    }
                }
                catch (Exception ex2)
                {
                    Message = ex2.Message;
                }
            }
        }
    }
}