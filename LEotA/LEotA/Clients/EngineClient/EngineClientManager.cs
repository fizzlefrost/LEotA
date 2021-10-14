#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json;
using LEotA.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LEotA.Clients.EngineClient
{
    public class EngineClientManager
    {
        private readonly IAboutUsPatron? _aboutUsPatron;

        public EngineClientManager(IServiceProvider serviceProvider)
        {
            _aboutUsPatron = serviceProvider.GetService<IAboutUsPatron>();
        }

        public List<AboutUs>? AboutUsGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes)
        {
            return _aboutUsPatron?.AboutUsGetPagedAsync(new CalabongaGetPagedRequestModel()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    SortDirection = sortDirection,
                    Search = search,
                    DisabledDefaultIncludes = disabledDefaultIncludes
                }).Result.Result.Items;
        } 
    }
}