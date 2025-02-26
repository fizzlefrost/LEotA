﻿using AutoMapper;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LEotA.Engine.Web.AppStart
{
    /// <summary>
    /// AutoMapper profiles registration
    /// </summary>
    public static class MapperRegistration
    {
        private static List<Type> GetProfiles() => (from t in typeof(Startup).GetTypeInfo().Assembly.GetTypes()
                                                    where typeof(IAutoMapper).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract
                                                    select t).ToList();

        /// <summary>
        /// Create and build mapper profiles
        /// </summary>
        /// <returns></returns>
        public static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = GetProfiles();
            return new MapperConfiguration(cfg =>
            {
                foreach (var item in profiles.Select(profile => (Profile)Activator.CreateInstance(profile)))
                {
                    cfg.AddProfile(item);
                }
            });
        }
    }
}