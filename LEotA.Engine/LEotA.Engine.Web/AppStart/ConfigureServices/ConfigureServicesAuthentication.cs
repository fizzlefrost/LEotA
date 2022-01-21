using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using LEotA.Engine.Data;
using LEotA.Engine.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LEotA.Engine.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// Authentication path
    /// </summary>
    public static class ConfigureServicesAuthentication
    {
        /// <summary>
        /// Configure Authentication & Authorization
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");
            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.SupportedTokens = SupportedTokens.Jwt;
                        options.Authority = url;
                        options.EnableCaching = true;
                        options.RequireHttpsMetadata = false;
                    })
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "190166849064-fa2j7v5eiej6918atu1m35b2e34fmipc.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

            services.AddIdentityServer(options =>
                {
                    options.Authentication.CookieSlidingExpiration = true;
                    options.IssuerUri = url;
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.UserInteraction.LoginUrl = "/Authentication/Login";
                    options.UserInteraction.LogoutUrl = "/Authentication/Logout";
                })
                .AddInMemoryPersistedGrants()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryApiScopes(IdentityServerConfig.GetAPiScopes())
                .AddAspNetIdentity<ApplicationUser>()
                .AddJwtBearerClientAuthentication()
                .AddProfileService<IdentityProfileService>();
        }
    }
}
