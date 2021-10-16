#nullable enable
using System;
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

        public LoginModel(ILogger<AdminPanelModel> logger, EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }
        
        public void OnGet()
        {
            Console.WriteLine("asdasd");
        }
        
        // public void OnPost()
        // {
        //     Console.WriteLine("asdasd");
        // }
        // public void OnGetSubmit()
        // {
        //     Console.WriteLine("asdasd");
        // }
    }
}