﻿using AutoMapper;

namespace LEotA.Engine.Web.Infrastructure.Mappers.Base
{
    /// <summary>
    /// Base class for mapper configuration.
    /// All ViewModel that will be mapped should implement IAutoMapper
    /// </summary>
    public abstract class MapperConfigurationBase : Profile, IAutoMapper { }
}
