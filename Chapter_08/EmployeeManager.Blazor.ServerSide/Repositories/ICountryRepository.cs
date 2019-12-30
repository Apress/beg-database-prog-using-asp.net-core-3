using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Blazor.ServerSide.Models;


namespace EmployeeManager.Blazor.ServerSide.Repositories
{
    public interface ICountryRepository
    {
        List<Country> SelectAll();
    }
}
