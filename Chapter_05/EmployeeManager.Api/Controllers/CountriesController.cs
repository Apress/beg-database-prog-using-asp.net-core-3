using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.Api.Repositories;


namespace EmployeeManager.Api.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {

        private readonly ICountryRepository countryRepository = null;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }


        [HttpGet]
        public List<Country> Get()
        {
            return countryRepository.SelectAll();
        }
    }
}
