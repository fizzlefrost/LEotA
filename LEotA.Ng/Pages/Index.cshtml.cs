#nullable enable
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        
        public IndexModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public void OnGet()
        {
            var newsList = _engineClientManager.NewsGetPagedAsync(null, 10, null, null, false);
            ViewData.Add("newsList", newsList);
        }
    }
}