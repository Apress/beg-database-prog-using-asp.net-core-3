using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ApiClient.Models
{
    public class WebApiConfig
    {
        public string BaseUrl { get; set; }
        public string EmployeesApiUrl { get; set; }
        public string CountriesApiUrl { get; set; }
    }
}
