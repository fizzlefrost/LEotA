#nullable enable
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LEotA.Clients.Claim;
using LEotA.Models.Claim;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace LEotA.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IActionResult> OnGet()
        {
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