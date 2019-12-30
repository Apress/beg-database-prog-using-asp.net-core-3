using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Blazor.ServerSide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Blazor.ServerSide.Repositories
{
    public class CountryRepository : ICountryRepository
    {

        private readonly AppDbContext db = null;

        public CountryRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<Country> SelectAll()
        {
            return db.Countries.ToList();
        }
    }
}
