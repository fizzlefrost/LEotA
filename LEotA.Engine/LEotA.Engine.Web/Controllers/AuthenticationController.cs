using System;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Services;
using LEotA.Engine.Data;
using LEotA.Engine.Web.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Engine.Web.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationController(
            IIdentityServerInteractionService interaction,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _interaction = interaction;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login() => View();

        [HttpPost("[action]")]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var signResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!signResult.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect password");
                return View(model);
            }
            
            return Redirect("https://172.18.0.2:5001/en/Login");
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            await _signInManager.SignOutAsync();
            return Redirect(logout.PostLogoutRedirectUri);
        }
        
        
        [HttpGet("google-login")]
        [Authorize(AuthenticationSchemes = "Google")]
        public async Task<IActionResult> GoogleLogin(string redirectUri)
        {
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var externalUser = result.Principal;
            if (externalUser == null)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims  of the external user
            var claims = externalUser.Claims.ToList();

            // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            var externalUserId = userIdClaim.Value;
            var externalProvider = userIdClaim.Issuer;

            // use externalProvider and externalUserId to find your user, or provision a new user
            return Redirect(redirectUri);
        }

        [Route("google-login-in")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GoogleLoginIn()
        {
            var properties = new AuthenticationProperties {RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        
        [Route("google-response")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);

        }
    }
}
