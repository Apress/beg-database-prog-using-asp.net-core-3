using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using EmployeeManager.MongoDB.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Driver;
using MongoDB.Bson;


namespace EmployeeManager.MongoDB.Controllers
{
    public class SecurityController : Controller
    {
        private IConfiguration config;
        private IMongoCollection<AppUser> users;

        public SecurityController(IConfiguration config)
        {
            this.config = config;

            var client = new MongoClient(config.GetValue<string>("MongoDBSettings:Server"));
            IMongoDatabase db = client.GetDatabase(config.GetValue<string>("MongoDBSettings:DatabaseName"));
            this.users = db.GetCollection<AppUser>(config.GetValue<string>("MongoDBSettings:UserCollectionName"));
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
                AppUser obj = this.users.Find(u => u.UserName == model.UserName).FirstOrDefault();

                if (obj != null)
                {
                    ModelState.AddModelError("", "UserName already exists!");
                }
                else
                {
                    AppUser user = new AppUser();
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.FullName = model.FullName;
                    user.BirthDate = model.BirthDate;
                    user.Role = "Manager";

                    this.users.InsertOne(user);

                    ViewData["message"] = "User Account Created Successfully!";
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
            AppUser obj = this.users.Find(u => u.UserName == model.UserName && u.Password == model.Password).FirstOrDefault();

            bool isUserValid = (obj == null ? false : true);

            if (ModelState.IsValid && isUserValid)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, obj.UserName));

                claims.Add(new Claim(ClaimTypes.Role, obj.Role));

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
        CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Security");
        }



        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}


