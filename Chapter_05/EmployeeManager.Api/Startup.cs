using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.Api.Models;
using EmployeeManager.Api.Repositories;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace EmployeeManager.Api
{
    public class Startup
    {
        private IConfiguration config = null;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(this.config.GetConnectionString("AppDb")));

            services.AddScoped<IEmployeeRepository, EmployeeSqlRepository>();

            services.AddScoped<ICountryRepository, CountrySqlRepository>();

            //services.AddScoped<IEmployeeRepository, EmployeeStProcRepository>();

            //services.AddScoped<ICountryRepository, CountryStProcRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints=> {
                endpoints.MapControllers();
            });

        }
    }
}
