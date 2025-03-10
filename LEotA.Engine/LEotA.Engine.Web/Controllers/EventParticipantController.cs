﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.EventParticipantViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventParticipantController: WritableController<EventParticipantViewModel, EventParticipant, EventParticipantCreateViewModel, EventParticipantUpdateViewModel, PagedListQueryParams>
    {
        public EventParticipantController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
        
        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ByUserIdAsync(Guid userId)
        {
            try
            {
                return Ok(Repository
                    .GetAll(true).Where(e => e.UserId == userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        public Expression<Func<EventParticipant, bool>> Expression { get; set; }
    }
}