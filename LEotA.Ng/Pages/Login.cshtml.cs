#nullable enable
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using LEotA.Models.Claim;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var model = new ClaimManager(HttpContext, User);
            
            ViewData.Add("tokens", model);
                
            // return Page();
            return Redirect("Index");
        }
        
        // public async Task OnPost(string email, string password, string passwordConfirm, string _RequestVerificationToken)
        // {
        //     // var ordersClient = _httpClientFactory.CreateClient();
        //     //
        //     // var response = await ordersClient.GetAsync("localhost:10001");
        //     //
        //     // var message = response.Content.ReadAsStreamAsync();
        //     //
        //     // ViewData["Message"] = message;
        // }
    }
}