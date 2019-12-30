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
    public class EmployeesController : Controller
    {

        private readonly IEmployeeRepository employeeRepository = null;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        [HttpGet]
        public List<Employee> Get()
        {
            return employeeRepository.SelectAll();
        }



        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeRepository.SelectByID(id);
        }


        [HttpPost]
        public void Post([FromBody]Employee emp)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Insert(emp);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Employee emp)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(emp);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeRepository.Delete(id);
        }
    }
}
