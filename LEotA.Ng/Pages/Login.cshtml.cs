#nullable enable
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<AdminPanelModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterViewModel model { get; set; }

        public LoginModel(ILogger<AdminPanelModel> logger, EngineClientManager engineClientManager, IHttpClientFactory _httpClientFactory)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
            this._httpClientFactory = _httpClientFactory;
        }
        
        public void OnGet()
        {
            
        }
        
        public async Task OnPost(string email, string password, string passwordConfirm, string _RequestVerificationToken)
        {
            var ordersClient = _httpClientFactory.CreateClient();
            
            var response = await ordersClient.GetAsync("localhost:10001");

            var message = response.Content.ReadAsStreamAsync();

            ViewData["Message"] = message;
        }
    }
}