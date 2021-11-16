using System;
using LEotA.Clients.EngineClient;
using LEotA.Clients.EngineClient.Patrons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using LEotA.Resources;
using LEotA.RouteModelConventions;
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
            Uri engineUrl = new Uri(Configuration.GetSection("EngineUrl").Value ?? throw new InvalidOperationException("Invalid engine url in appsettings.json"));
            // var builder = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json"));
            // var _Configuration = builder.Build();
            // services.Configure<ApplicationSettings>(sectionApplicationSettings);
            // var applicationSettings = sectionApplicationSettings.Get<ApplicationSettings>();
            
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
            services.AddHttpClient();
            services.AddSingleton<EngineClientManager>();
            services.AddSingleton<EngineAuthenticationManager>();
            services.AddSingleton<CommonLocalizationService>();
            
            services.Configure<GzipCompressionProviderOptions>
                (options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            
            // i know i know
            services.AddHttpClient<IAboutUsPatron, AboutUsPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            services.AddHttpClient<IAlbumPatron, AlbumPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            // services.AddHttpClient<IEventParticipantPatron, EventParticipantPatron>(client =>
            // {
            //     client.BaseAddress = new Uri(engineUrl);
            // });
            services.AddHttpClient<IEventPatron, EventPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            services.AddHttpClient<IFileContentPatron, FileContentPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            services.AddHttpClient<INewsPatron, NewsPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            services.AddHttpClient<IPublicationPatron, PublicationPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            services.AddHttpClient<IProjectPatron, ProjectPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            services.AddHttpClient<IStaffPatron, StaffPatron>(client =>
            {
                client.BaseAddress = engineUrl;
            });
            
            services.AddHttpClient();

            services.AddAuthentication(config =>
                {
                    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = "https://localhost:10001";
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
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            
            app.UseResponseCompression();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            app.UseRequestLocalization(localizationOptions);
            
            var options = new RewriteOptions().Add(new FirstLoadRewriteRule());
            app.UseRewriter(options);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}