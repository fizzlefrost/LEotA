using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Clients.EngineClient.Patrons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using LEotA.Data;
using LEotA.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LEotA
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
            // var sectionApplicationSettings = Configuration.GetSection("App");
            // services.Configure<ApplicationSettings>(sectionApplicationSettings);
            // var applicationSettings = sectionApplicationSettings.Get<ApplicationSettings>();
            
            services.AddRazorPages();
            services.AddHttpClient();
            
            services.AddSingleton<EngineClientManager>();
            
            services.AddHttpClient<IAboutUsPatron, AboutUsPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });

            services.AddAuthentication("Cookie")
                .AddCookie("Cookie", config =>
                {
                    config.LoginPath = "/AdminPanel";
                });
            services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}