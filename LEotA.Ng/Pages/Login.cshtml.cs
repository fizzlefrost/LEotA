#nullable enable
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
        }
        public async Task<IActionResult> OnGet()
        {
            return Redirect("Index");
        }
    }
}