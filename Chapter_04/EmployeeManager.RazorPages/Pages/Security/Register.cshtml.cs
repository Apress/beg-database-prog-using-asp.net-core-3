using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManager.RazorPages.Models;
using EmployeeManager.RazorPages.Security;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManager.RazorPages.Pages.Security
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Register RegisterData { get; set; }

        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;


        public RegisterModel(UserManager<AppIdentityUser> userManager,
            RoleManager<AppIdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
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
                user.UserName = RegisterData.UserName;
                user.Email = RegisterData.Email;
                user.FullName = RegisterData.FullName;
                user.BirthDate = RegisterData.BirthDate;

                IdentityResult result = await userManager.CreateAsync
                (user, RegisterData.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                    return RedirectToPage("/Security/SignIn");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details!");
                }
            }
            return Page();
        }


    }
}