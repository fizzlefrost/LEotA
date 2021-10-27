#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
            var news = _engineClientManager.NewsGetById(id);
            ViewData.Add("newsId", news);
            return Page(); 
        }
    }
}