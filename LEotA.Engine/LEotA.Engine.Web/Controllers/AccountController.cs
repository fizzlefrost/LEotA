using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork.Controllers.Controllers;
using LEotA.Engine.Web.Infrastructure.Auth;
using LEotA.Engine.Web.Infrastructure.Services;
using LEotA.Engine.Web.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LEotA.Engine.Web.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class AccountController : OperationResultController
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Register controller
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService) => _accountService = accountService;

        /// <summary>
        /// Register new user. Success registration returns UserProfile for new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        // public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Register([FromBody] RegisterViewModel model)
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return OperationResultResponse(await _accountService.RegisterAsync(model));
        }

        /// <summary>
        /// Returns profile information for authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return OperationResultError<UserProfileViewModel>(null, new MicroserviceUnauthorizedException());
            }

            var userId = _accountService.GetCurrentUserId();
            if (Guid.Empty == userId)
            {
                return BadRequest();
            }

            var claimsOperationResult = await _accountService.GetProfileByIdAsync(userId.ToString());
            return Ok(claimsOperationResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> Register() => View();
    }
}