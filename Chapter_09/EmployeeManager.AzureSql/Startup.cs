using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManager.AzureSql.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.AzureSql.Security;
using Microsoft.AspNetCore.Identity;
using EmployeeManager.AzureSql.Repositories;
using Microsoft.Extensions.Hosting;


namespace EmployeeManager.AzureSql
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
            services.AddControllersWithViews();

            services.AddDbContext<AppIdentityDbContext>(options =>
        options.UseSqlServer(this.config.GetConnectionString("AppDb")));


            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();


            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Security/SignIn";
                opt.AccessDeniedPath = "/Security/AccessDenied";
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=EmployeeManager}/{action=List}/{id?}");
            });

        }
    }
}
