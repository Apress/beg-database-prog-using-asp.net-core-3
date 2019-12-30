using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManager.RazorPages.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.RazorPages.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;


namespace EmployeeManager.RazorPages
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
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/EmployeeManager/List", "");
                });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(this.config.GetConnectionString("AppDb")));

            services.AddDbContext<AppIdentityDbContext>(options =>
 options.UseSqlServer(this.config.GetConnectionString
 ("AppDb")));

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

            app.UseStatusCodePagesWithReExecute("/Error","?code={0}");

            app.UseExceptionHandler("/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints=> {
                endpoints.MapRazorPages();
            });

        }
    }
}
