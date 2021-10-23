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
        private readonly ILogger<EventsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public string _title;
        public CalabongaViewModel<NewsColumn>? _newsColumnList { get; set; }
        public List<NewsViewModel> newsColumnModelList { get; set; }

        public NewsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public IActionResult OnGet(Guid? id)
        {
            if (id.HasValue)
            {
                _newsColumnList = _engineClientManager.NewsColumnGetById(id.Value);
            }
            newsColumnModelList = new List<NewsViewModel>();
            return Page();
        }
    }
}