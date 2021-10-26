using System.Threading.Tasks;
using IdentityModel;
using LEotA.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace LEotA.Pages
{
    public class Logout : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Index");
        }
    }
}