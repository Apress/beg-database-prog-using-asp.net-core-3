using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManager.Blazor.ServerSide.Models;
using Microsoft.AspNetCore.Identity;
using EmployeeManager.Blazor.ServerSide.Security;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Blazor.ServerSide.Pages.Security
{
    [Authorize]
    public class SignOutModel : PageModel
    {
        private readonly SignInManager<AppIdentityUser> signinManager;

        public SignOutModel(SignInManager<AppIdentityUser> signinManager)
        {
            this.signinManager = signinManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await signinManager.SignOutAsync();
            return RedirectToPage("/Security/SignIn");
        }
    }
}