using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManager.Angular.Controllers
{
    public class SpaController : Controller
    {
        public IActionResult Index([FromServices]IWebHostEnvironment env)
        {
            return new PhysicalFileResult(env.WebRootPath + "/index.html", "text/html");
        }
    }
}
