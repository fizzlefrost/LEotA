﻿using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.StaffViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class StaffController: WritableController<StaffViewModel, Staff, StaffCreateViewModel, StaffUpdateViewModel, PagedListQueryParams>
    {
        public StaffController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
            
        }
    }
}