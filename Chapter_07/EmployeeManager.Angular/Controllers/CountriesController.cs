using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Angular.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Angular.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class CountriesController : ControllerBase
    {

        private readonly AppDbContext db = null;

        public CountriesController(AppDbContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Country> countries = await db.Countries.ToListAsync();
            return Ok(countries);
        }
    }
}
