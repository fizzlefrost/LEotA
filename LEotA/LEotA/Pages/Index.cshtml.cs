using System.Collections.Generic;
using LEotA.Clients;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class TestModel : PageModel
    {
        private readonly ILogger<TestModel> _logger;

        public TestModel(ILogger<TestModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }
        
        public static string OnPostAboutUs(List<string> AmogusList)
        {
            var message = AmogusList[0];
            var name = AmogusList[1];
            return (EngineClient.PostAboutUs(message, name).Result);
        }
    }
}