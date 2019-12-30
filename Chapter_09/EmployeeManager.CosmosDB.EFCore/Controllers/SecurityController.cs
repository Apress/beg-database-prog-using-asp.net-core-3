using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using EmployeeManager.CosmosDB.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace EmployeeManager.CosmosDB.Controllers
{
    public class SecurityController : Controller
    {
        private readonly AppDbContext db;

        public SecurityController(AppDbContext db)
        {
            this.db = db;
            db.Database.EnsureCreated();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                AppUser usr = (from u in db.Users
                               where u.UserName == model.UserName
                               select u).SingleOrDefault();

                if (usr != null)
                {
                    ModelState.AddModelError("", "UserName already exists!");
                }
                else
                {
                    AppUser user = new AppUser();
                    user.DocumentID = Guid.NewGuid();
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.FullName = model.FullName;
                    user.BirthDate = model.BirthDate;
                    user.Role = "Manager";

                    db.Users.Add(user);
                    db.SaveChanges();

                    ViewData["message"] = "User created successfully!";
                }
            }
            return View();
        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignIn model)
        {

            AppUser usr = (from u in db.Users
                           where u.UserName == model.UserName && u.Password == model.Password
                           select u).SingleOrDefault();

            bool isUserValid = (usr == null ? false : true);

            if (ModelState.IsValid && isUserValid)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usr.UserName));

                claims.Add(new Claim(ClaimTypes.Role, usr.Role));

                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.
        AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = model.RememberMe;

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.
        AuthenticationScheme,
                    principal, props).Wait();

                return RedirectToAction("List", "EmployeeManager");
            }
            else
            {
                ModelState.AddModelError("","Invalid UserName         or Password!");
            }

            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("SignIn", "Security");
        }



        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}


