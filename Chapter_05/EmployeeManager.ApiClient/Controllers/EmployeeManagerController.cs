using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.ApiClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace EmployeeManager.ApiClient.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string employeesApiUrl = "";
        private string countriesApiUrl = "";

        //private WebApiConfig config = null;

        public EmployeeManagerController(HttpClient client, IConfiguration config)
        {
            this.client = client;
            employeesApiUrl = config.GetValue<string>("AppSettings:EmployeesApiUrl");
            countriesApiUrl = config.GetValue<string>("AppSettings:CountriesApiUrl");

            //add IOptions<WebApiConfig> parameter to 
            //constructor and uncomment the following code
            //this.config = options.Value;
            //employeesApiUrl = this.config.EmployeesApiUrl;
            //countriesApiUrl = this.config.CountriesApiUrl;
        }

        public async Task<bool> FillCountriesAsync()
        {
            HttpResponseMessage response = await client.GetAsync(countriesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Country> listCountries = JsonSerializer.Deserialize<List<Country>>(stringData,options);
            List<SelectListItem> countries = (from c in listCountries select new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();
            ViewBag.Countries = countries;
            return true;
        }

        public async Task<IActionResult> ListAsync()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringData =await response.Content.
        ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Employee> data = JsonSerializer.Deserialize<List<Employee>>(stringData, options);

            return View(data);
        }



        public async Task<IActionResult> InsertAsync()
        {
            await FillCountriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertAsync(Employee model)
        {
            await FillCountriesAsync();
            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync
            (employeesApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Employee inserted successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling Web API!";
                }
            }
            return View(model);
        }


        public async Task<IActionResult> UpdateAsync(int id)
        {
            await FillCountriesAsync();

            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Employee model)
        {
            await FillCountriesAsync();

            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync
            ($"{employeesApiUrl}/{model.EmployeeID}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Employee updated successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling Web API!";
                }
            }
            return View(model);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int employeeID)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{employeesApiUrl}/{employeeID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Employee deleted successfully!";
            }
            else
            {
                TempData["Message"] = "Error while calling Web API!";
            }
            return RedirectToAction("List");
        }
    }
}
