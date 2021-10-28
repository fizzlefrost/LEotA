#nullable enable
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public PaginatedList<News> News { get; set; }

        public void OnGet()
        {
            var newsList = _engineClientManager.NewsGetPagedAsync(null, 10, null, null, false);
            ViewData.Add("newsList", newsList);
        }

        public async Task OnGetAsync(int page, int size)
        {
            var skip = (page + 1);
            var take = size;
            var newsList = _engineClientManager.NewsGetPaged(skip, take, null, null, false);
            ViewData.Add("newsList", newsList);
        }
    }
}