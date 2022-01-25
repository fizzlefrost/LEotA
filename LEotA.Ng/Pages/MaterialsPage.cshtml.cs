using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class MaterialsPage : PageModel
    {
        [Authorize]
        public void OnGet()
        {
            User.IsAuthenticated();
        }
    }
}