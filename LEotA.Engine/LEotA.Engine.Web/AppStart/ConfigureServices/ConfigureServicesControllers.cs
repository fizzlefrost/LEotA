﻿using Microsoft.Extensions.DependencyInjection;

namespace LEotA.Engine.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// Configure controllers
    /// </summary>
    public static class ConfigureServicesControllers
    {
        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services) => services.AddControllersWithViews();
    }
}
