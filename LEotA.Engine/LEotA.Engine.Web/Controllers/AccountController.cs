﻿using Calabonga.OperationResults;
using LEotA.Engine.Web.Infrastructure.Auth;
using LEotA.Engine.Web.Mediator.Account;
using LEotA.Engine.Web.ViewModels.AccountViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LEotA.Engine.Web.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Register controller
        /// </summary>
        public AccountController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Register new user. Success registration returns UserProfile for new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Register([FromBody] RegisterViewModel model) =>
            Ok(await _mediator.Send(new RegisterRequest(model), HttpContext.RequestAborted));

        /// <summary>
        /// Returns profile information for authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Profile() =>
            await _mediator.Send(new ProfileRequest(), HttpContext.RequestAborted);
    }
}