using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Cosmos;
using EmployeeManager.CosmosDB.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManager.CosmosDB
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

            services.AddDbContext<AppDbContext>(options =>
                options.UseCosmos(
                config.GetValue<string>("CosmosDBSettings:Server"),
                config.GetValue<string>("CosmosDBSettings:PrimaryKey"),
                config.GetValue<string>("CosmosDBSettings:DatabaseName")
            ));

            services.AddAuthentication
        (CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(o =>
        {
            o.LoginPath = "/Security/SignIn";
            o.AccessDeniedPath = "/Security/AccessDenied";
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
