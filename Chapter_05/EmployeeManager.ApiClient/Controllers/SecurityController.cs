using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using EmployeeManager.ApiClient.Security;
using EmployeeManager.ApiClient.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace EmployeeManager.ApiClient.Controllers
{
    public class SecurityController : Controller
    {

        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly SignInManager<AppIdentityUser> signinManager;

        public SecurityController(UserManager<AppIdentityUser> userManager, RoleManager<AppIdentityRole> roleManager,
            SignInManager<AppIdentityUser> signinManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(Register obj)
        {
            if (ModelState.IsValid)
            {
                if (!await roleManager.RoleExistsAsync("Manager"))
                {
                    AppIdentityRole role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can perform CRUD operations.";
                    IdentityResult roleResult = await roleManager.
                    CreateAsync(role);
                }

                AppIdentityUser user = new AppIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;

                IdentityResult result = await userManager.CreateAsync
                (user, obj.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details");
                }
            }
            return View(obj);
        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInAsync(SignIn obj)
        {
            if (ModelState.IsValid)
            {
                var result = await signinManager.PasswordSignInAsync
                (obj.UserName, obj.Password,
                    obj.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("List", "EmployeeManager");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details");
                }
            }
            return View(obj);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOutAsync()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("SignIn", "Security");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}


