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
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;


namespace EmployeeManager.CosmosDB.Controllers
{
    public class SecurityController : Controller
    {
        private DocumentClient client;
        private Uri userCollectionUri;
        private string databaseName;
        private string userCollectionName;

        public SecurityController(IConfiguration config)
        {
            var uri = new Uri(config.GetValue<string>("CosmosDBSettings:Server"));
            var primaryKey = config.GetValue<string>("CosmosDBSettings:PrimaryKey");
            databaseName = config.GetValue<string>("CosmosDBSettings:DatabaseName");
            userCollectionName = config.GetValue<string>("CosmosDBSettings:UserCollectionName");

            client = new DocumentClient(uri, primaryKey);

            this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName }).Wait();

            this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = userCollectionName }).Wait();

            this.userCollectionUri = UriFactory
                    .CreateDocumentCollectionUri(
                        databaseName, userCollectionName);

        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                AppUser usr = client.CreateDocumentQuery<AppUser>(userCollectionUri).Where(u => u.UserName == model.UserName).AsEnumerable().SingleOrDefault();

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

                    await client.CreateDocumentAsync(userCollectionUri, user);
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
        public async Task<IActionResult> SignIn(SignIn model)
        {

            AppUser usr = client.CreateDocumentQuery<AppUser>(userCollectionUri).Where(u => u.UserName == model.UserName && u.Password==model.Password).AsEnumerable().SingleOrDefault();

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

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.
        AuthenticationScheme,
                    principal, props);

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
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Security");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}


