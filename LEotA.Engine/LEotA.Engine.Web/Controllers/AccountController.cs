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
using IdentityServer4.Services;
using LEotA.Engine.Data;
using Microsoft.AspNetCore.Identity;

namespace LEotA.Engine.Web.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class AccountController : OperationResultController
    {
        private readonly IAccountService _accountService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Register controller
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService,
            IIdentityServerInteractionService interaction,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _interaction = interaction;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Register new user. Success registration returns UserProfile for new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(OperationResult<UserProfileViewModel>))]
        public async Task<ActionResult<OperationResult<UserProfileViewModel>>> Register([FromBody] RegisterViewModel model)
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
        
        // [AllowAnonymous]
        // [HttpGet("[action]")]
        // public async Task<IActionResult> Login() => View();
        
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> Login(string returnUrl) => View();
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please. Validate your credentials and try again.");
                return View(model);
            }
        
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }
        
            var signResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!signResult.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        
            return Redirect(model.ReturnUrl);
        }
    }
}