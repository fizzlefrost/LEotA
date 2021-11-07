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