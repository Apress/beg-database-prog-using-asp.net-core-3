using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Jquery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Jquery.Controllers
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
        public async Task<List<Country>> GetAsync()
        {
            List<Country> countries=await db.Countries.ToListAsync();
            return countries;
        }
    }
}
