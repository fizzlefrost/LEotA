#nullable enable
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using JW;
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
        public int TotalPages = 1;
        public int PageSize = 10;
        public IEnumerable<string> Items { get; set; }
        public Pager Pager { get; set; }
        public IndexModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        
        public void OnGet(int p)
        {
            switch (p)
            {
                case 0: 
                    TotalPages = _engineClientManager.NewsGetTotalPages(PageSize).Result;
                    OnGet(1);
                    break;
                default:
                    TotalPages = _engineClientManager.NewsGetTotalPages(PageSize).Result;

                    // get pagination info for the current page
                    Pager = new Pager(TotalPages, p);

                    // assign the current page of items to the Items property
                    Items = _engineClientManager.NewsGetPagedAsync(p - 1, 10, null, null, false).Result!.Select(n=>n.Name.English);
            
                    // ViewData.Add("newsList", newsList);
                    break;
            }
        }
    }
}