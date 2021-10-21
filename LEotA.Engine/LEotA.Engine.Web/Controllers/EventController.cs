using System;
using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.EventViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventController: WritableController<EventViewModel, Event, EventCreateViewModel, EventUpdateViewModel, PagedListQueryParams>
    {

        public EventController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
    }
}