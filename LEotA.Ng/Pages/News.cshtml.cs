#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var newsWithImage = new Dictionary<News, List<FileContent>?>();
            ViewData.Clear();
            var news = _engineClientManager.NewsGetById(id)?.Result;
            // if (news != null)
            // {
            //     var newsImage = _engineClientManager.FileContentGetByMasterId(news.Id);
            //     newsWithImage.Add(news, newsImage);
            //     ViewData.Add("newsId",newsWithImage);
            // }
            ViewData.Add("newsId", news);
            return Page(); 
        }
    }
}