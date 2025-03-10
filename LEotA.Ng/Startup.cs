using System;
using LEotA.Clients.EngineClient;
using LEotA.Clients.EngineClient.Patrons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO.Compression;
using System.Net.Http;
using System.Security.Authentication;
using LEotA.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Extensions.Logging;

namespace LEotA
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Console.WriteLine($"ContentRootPath: {env.ContentRootPath}");
            Console.WriteLine($"WebRootPath: {env.WebRootPath}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // Check if the environment is "dev" and the local settings file exists
            if (env.EnvironmentName == "Development")
            {
                var localAppSettingsPath = Path.Combine(env.ContentRootPath, "appsettings.local.json");
                if (File.Exists(localAppSettingsPath))
                {
                    builder.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
                }
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Uri engineUrl = new Uri(Configuration.GetSection("EngineUrl").Value ?? throw new InvalidOperationException("Invalid engine url in appsettings.json check"));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddRazorPages(options => {
                options.Conventions.AddFolderRouteModelConvention("/", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        selector.AttributeRouteModel = new AttributeRouteModel
                        {
                            Order = -1,
                            Template = AttributeRouteModel.CombineTemplates(
                                "{culture?}",
                                selector.AttributeRouteModel.Template),
                        };
                    }
                });
            });

            services.AddTransient<IAboutUsPatron, AboutUsPatron>();
            services.AddTransient<IAlbumPatron, AlbumPatron>();
            services.AddTransient<IEventParticipantPatron, EventParticipantPatron>();
            services.AddTransient<IEventPatron, EventPatron>();
            services.AddTransient<IFileContentPatron, FileContentPatron>();
            services.AddTransient<INewsPatron, NewsPatron>();
            services.AddTransient<IProjectPatron, ProjectPatron>();
            services.AddTransient<IPublicationPatron, PublicationPatron>();
            services.AddTransient<IStaffPatron, StaffPatron>();
            
            services.AddScoped<EngineClientManager>();
            services.AddSingleton<EngineAuthenticationManager>();
            services.AddSingleton<CommonLocalizationService>();
            
            services.Configure<GzipCompressionProviderOptions>
                (options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            
            
            services.AddHttpClient("Engine", config =>
            {
                config.BaseAddress = engineUrl;
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    },
                AllowAutoRedirect = false,
                //ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
                SslProtocols = SslProtocols.Tls
            });

            services.AddAuthentication(config =>
                {
                    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = Configuration.GetSection("EngineHttpsUrl").Value;
                    config.ClientId = "leota_client_id";
                    config.ClientSecret = "leota_client_secret";
                    config.SaveTokens = true;

                    config.ResponseType = "code";

                    config.GetClaimsFromUserInfoEndpoint = true;
                });
            services.AddAuthorization();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "ru", uiCulture:"ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider { Options = options });
            });
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += 
                    (sender, certificate, chain, sslPolicyErrors) => true;
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += 
                    (sender, certificate, chain, sslPolicyErrors) => true;
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCompression();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            app.UseRequestLocalization(localizationOptions);
            
            var options = new RewriteOptions().Add(new FirstLoadRewriteRule());
            app.UseRewriter(options);
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}