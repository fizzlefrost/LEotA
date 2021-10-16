#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public AboutUsModel(ILogger<AboutUsModel> logger, EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }
        public void OnGet()
        {
            //_aboutUsList = _engineClientManager.AboutUsGetPaged(null, 10, null, null, false);
        }
        
    }
}