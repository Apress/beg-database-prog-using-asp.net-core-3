using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace EmployeeManager.Jquery.Controllers
{
    public class EmployeeManagerController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            ViewBag.EmployeeID = id;
            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewBag.EmployeeID = id;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

    }
}
