using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.AzureSql.Models;


namespace EmployeeManager.AzureSql.Repositories
{
    public interface ICountryRepository
    {
        List<Country> SelectAll();
    }
}
