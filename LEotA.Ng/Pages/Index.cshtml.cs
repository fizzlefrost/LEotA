#nullable enable
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            return Redirect("/" + culture + "/mainpage");
        }
        
    }
}