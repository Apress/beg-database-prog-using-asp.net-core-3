using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Api.Models;


namespace EmployeeManager.Api.Repositories
{
    public interface ICountryRepository
    {
        List<Country> SelectAll();
    }
}
