using System;
using LEotA.Clients.EngineClient;
using LEotA.Clients.EngineClient.Patrons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Reflection;
using LEotA.Resources;
using LEotA.RouteModelConventions;
using LEotA.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
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
            // var sectionApplicationSettings = Configuration.GetSection("App");
            // services.Configure<ApplicationSettings>(sectionApplicationSettings);
            // var applicationSettings = sectionApplicationSettings.Get<ApplicationSettings>();
            
            services.AddRazorPages(options => {
                options.Conventions.Add(new CultureTemplatePageRouteModelConvention());
            });
            services.AddHttpClient();
            services.AddSingleton<EngineClientManager>();
            services.AddSingleton<CommonLocalizationService>();
            
            // i know i know
            services.AddHttpClient<IAboutUsPatron, AboutUsPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });
            services.AddHttpClient<IAlbumPatron, AlbumPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });
            // services.AddHttpClient<IEventParticipantPatron, EventParticipantPatron>(client =>
            // {
            //     client.BaseAddress = new Uri("https://localhost:10001");
            // });
            services.AddHttpClient<IEventPatron, EventPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });
            services.AddHttpClient<IImagePatron, ImagePatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });
            services.AddHttpClient<INewsPatron, NewsPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });
            services.AddHttpClient<IPublicationPatron, PublicationPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
            });
            services.AddHttpClient<IProjectPatron, ProjectPatron>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:10001");
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
            services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(CommonResources).GetTypeInfo().Assembly.FullName!);
                    return factory.Create(nameof(CommonResources), assemblyName.Name!);
                };
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}