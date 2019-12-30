using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmployeeManager.Blazor.ServerSide.Repositories;
using EmployeeManager.Blazor.ServerSide.Security;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.Blazor.ServerSide.Models;

namespace EmployeeManager.Blazor.ServerSide
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("AppDb")));

            services.AddDbContext<AppIdentityDbContext>(options =>
        options.UseSqlServer(this.Configuration.GetConnectionString
        ("AppDb")));

            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            //=============
            // Uncomment to use policy based authorization
            //=============
            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy("MustBeManager",
            //            policy =>
            //            {
            //                policy.RequireRole("Manager");
            //            }
            //        );
            //});
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
