#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        public int TotalPages = 1;
        public int PageSize = 3;

        
        public IEnumerable<string> Items { get; set; }
        public IEnumerable<string> Description { get; set; }
        public IEnumerable<Guid> Id { get; set; }
        public Pager Pager { get; set; }
        public IndexModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        
        public async Task OnGet(int p)
        {
            try
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
                        Id = _engineClientManager.NewsGetPagedAsync(p - 1, 10, null, null, false).Result!.Select(n =>
                            n.Id);

                        if (HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name == "ru")
                        {
                            Items = _engineClientManager.NewsGetPagedAsync(p - 1, 10, null, null, false).Result!.Select(
                                n =>
                                    n.Name.Russian);
                            Description = _engineClientManager.NewsGetPagedAsync(p - 1, 10, null, null, false).Result!
                                .Select(n =>
                                    n.Description.Russian);
                        }
                        else
                        {
                            Items = _engineClientManager.NewsGetPagedAsync(p - 1, 10, null, null, false).Result!.Select(
                                n => n.Name.English);
                            Description = _engineClientManager.NewsGetPagedAsync(p - 1, 10, null, null, false).Result!
                                .Select(n =>
                                    n.Description.English);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                // do smth
            }
            
        }
    }
}