using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManager.Api.Repositories
{
    public class CountrySqlRepository : ICountryRepository
    {

        private readonly AppDbContext db = null;

        public CountrySqlRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<Country> SelectAll()
        {
            List<Country> data = db.Countries.FromSqlRaw("SELECT CountryID, Name FROM Countries ORDER BY Name ASC").ToList();
            return data;
        }

    }
}
