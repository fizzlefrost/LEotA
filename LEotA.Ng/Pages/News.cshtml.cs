#nullable enable
using System;
using System.Linq;
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class NewsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;

        public NewsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public IActionResult OnGet(Guid id)
        {
            ViewData.Clear();
            var news = _engineClientManager.NewsGetById(id)?.Result;
            ViewData.Add("newsId", news);
            return Page(); 
        }
    }
}