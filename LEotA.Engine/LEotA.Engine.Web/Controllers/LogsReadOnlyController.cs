﻿using LEotA.Engine.Entities.Core;
using LEotA.Engine.Web.Infrastructure.Auth;
using LEotA.Engine.Web.Mediator.LogsReadonly;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LEotA.Engine.Web.Controllers
{
    /// <summary>
    /// ReadOnlyController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class LogsReadonlyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsReadonlyController(IMediator mediator) => _mediator = mediator;

        [HttpGet("[action]")]
        [Authorize(Policy = "Logs:UserRoles:View", Roles = AppData.SystemAdministratorRoleName)]
        public async Task<IActionResult> GetRoles() =>
            //Get Roles for current user
            Ok(await _mediator.Send(new GetRolesRequest(), HttpContext.RequestAborted));
    }
}