using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManager.RazorPages.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.RazorPages.Pages
{
    [Authorize(Roles = "Manager")]
    public class DeleteModel : PageModel
    {
        private AppDbContext db = null;

        [BindProperty]
        public Employee Employee { get; set; }

        public string Message = "";

        public bool dataFound = true;

        public DeleteModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet(int id)
        {
            Employee = db.Employees.Find(id);

            if (Employee == null)
            {
                this.dataFound = false;
                this.Message = "EmployeeID Not Found.";
            }
            else
            {
                this.dataFound = true;
                this.Message = "";
            }
        }


        public IActionResult OnPost()
        {
            Employee emp = db.Employees.Find(Employee.EmployeeID);
            try
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                TempData["Message"] = "Employee deleted successfully!";
                return RedirectToPage("/EmployeeManager/List");
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
            return Page();
        }
    }
}