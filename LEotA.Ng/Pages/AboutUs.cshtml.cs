#nullable enable
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public List<AboutUs>? _aboutUsList { get; set; }
        public AboutUsModel(ILogger<AboutUsModel> logger, EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }
        public void OnGet()
        {
            _aboutUsList = _engineClientManager.AboutUsGetPaged(null, 1, null, null, false);
        }
        
    }
}