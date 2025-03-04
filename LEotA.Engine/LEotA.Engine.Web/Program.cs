// ---------------------------------------
// Name: Microservice Template for ASP.NET Core API
// Author: Calabonga © Calabonga SOFT
// Version: 5.0.5
// Based on: .NET 5.0.x
// Created Date: 2019-10-06
// Updated Date 2021-06-04
// ---------------------------------------
// Contacts
// ---------------------------------------
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga
// YouTube: https://youtube.com/sergeicalabonga
// ---------------------------------------
// Description:
// ---------------------------------------
// This template implements Web API and IdentityServer functionality.
// Also, support two type Authentications: Cookie and Bearer.
// ---------------------------------------

using LEotA.Engine.Data.DatabaseInitialization;
using LEotA.Engine.Entities.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LEotA.Engine.Web
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var webHost = CreateHostBuilder(args).Build();
                using (var scope = webHost.Services.CreateScope())
                {
                    DatabaseInitializer.Seed(scope.ServiceProvider);
                }

                Console.Title = $"{AppData.ServiceName} v.{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
                await webHost.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;

                        config.SetBasePath(Directory.GetCurrentDirectory());

                        // Загружаем стандартные файлы конфигурации
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                        // Загружаем appsettings.local.json только в Development
                        if (env.IsDevelopment())
                        {
                            config.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
                        }

                        config.AddEnvironmentVariables();
                    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
