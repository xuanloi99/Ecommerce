using Ecommerce.ExtendMethod;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EcommerceDbContext>(options =>
            {
                string connectString = Configuration.GetConnectionString("Ecommerce");
                options.UseMySQL(connectString);
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            //config path of M-V-C
            services.Configure<RazorViewEngineOptions>(options =>
            {
                string extension = RazorViewEngine.ViewExtension;
                //{0}- action  {1} controller {2} Area
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + extension);
            });
            services.AddSingleton<ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Config error 400-500
            app.AddStatusCodePage();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //ProductManager
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "product",
                    pattern: "{controller}/{action=Index}/{id?}",
                    areaName: "ProductManager");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
