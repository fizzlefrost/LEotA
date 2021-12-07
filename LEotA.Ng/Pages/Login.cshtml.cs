#nullable enable
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGet(string returnUrl)
        {
            if (returnUrl == null)
                returnUrl = "Index";
            return Redirect(returnUrl);
        }
    }
}