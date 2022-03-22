#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileContent = LEotA.Models.FileContent;

namespace LEotA.Pages
{
    public class MainPageModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        public int cPage;
        public int TotalPages;
        public int TotalItems;
        public int PageSize = 4;
        public bool isFirst = true;
        
        public Pager Pager { get; set; }
        public MainPageModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        
        public async Task OnGet(int p)
        {
            
            //try
            //{
                switch (p)
                {
                    case 0:
                        TotalPages = _engineClientManager.NewsGetTotalPages(PageSize).Result;
                        TotalItems = _engineClientManager.NewsGetTotalPages(1).Result;
                        cPage = p;
                        OnGet(1);
                        break;
                    default:
                        TotalPages = _engineClientManager.NewsGetTotalPages(PageSize).Result;
                        TotalItems = _engineClientManager.NewsGetTotalPages(1).Result;
                        var newsUnsorted = _engineClientManager.NewsGetPagedAsync(null, TotalItems, null, null, false).Result;
                        var newsSorted = (newsUnsorted ?? throw new InvalidOperationException()).OrderByDescending(o => o.Time).ToList();
                        cPage = p;
                        // get pagination info for the current page
                        Pager = new Pager(TotalPages, p, PageSize);
                        
                        // assign the current page of items to the Items property
                        var newsGetPaged = newsSorted.GetRange((p - 1) * 4,(TotalItems<p*4)? TotalItems%4 : 4);
                        var newsListWithImage = new Dictionary<News, List<FileContent>>();
                        foreach (var news in newsGetPaged)
                        {
                            var newsListImage = _engineClientManager.FileContentGetByMasterId(news.Id);
                            newsListWithImage.Add(news, newsListImage);
                        }
                        ViewData.Add("newsListWithImage",newsListWithImage);
                        ViewData.Add("newsGetPaged", newsGetPaged);
                        ViewData.Add("newsGetPagedCount", newsGetPaged!.Count());
                        break;

                    // ViewData.Add("id",Id.ToList());
                        // if (HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name == "ru")
                        // {
                        //     foreach (var newsId in Id.ToList())
                        //     {
                        //         ViewData.Add(newsId.ToString(), new News
                        //         {
                        //             Id = newsId
                        //         });
                        //     }
                        //     
                        //     Names = _engineClientManager.NewsGetPagedAsync(p - 1, PageSize, null, null, false).Result!.Select(
                        //         n =>
                        //             n.Name.Russian);
                        //     foreach (var newsId in Id.ToList())
                        //     {
                        //         ViewData.Add(newsId.ToString(), new Dictionary<string, IEnumerable<string>>
                        //         {
                        //             ["news"] = Names
                        //         });
                        //     }
                        //     
                        //     Description = _engineClientManager.NewsGetPagedAsync(p - 1, PageSize, null, null, false).Result!
                        //         .Select(n =>
                        //             n.Description.Russian);
                        //     foreach (var newsId in Id.ToList())
                        //     {
                        //         ViewData.Add(newsId.ToString(), new Dictionary<string, IEnumerable<string>>
                        //         {
                        //             ["desc"] = Description
                        //         });
                        //     }
                        // }
                        // else
                        // {
                        //     Names = _engineClientManager.NewsGetPagedAsync(p - 1, PageSize, null, null, false).Result!.Select(
                        //         n => n.Name.English);
                        //     foreach (var newsId in Id.ToList())
                        //     {
                        //         ViewData.Add(newsId.ToString(), new Dictionary<string, IEnumerable<string>>
                        //         {
                        //             ["news"] = Names
                        //         });
                        //     }
                        //     Description = _engineClientManager.NewsGetPagedAsync(p - 1, PageSize, null, null, false).Result!
                        //         .Select(n =>
                        //             n.Description.English);
                        //     foreach (var newsId in Id.ToList())
                        //     {
                        //         ViewData.Add(newsId.ToString(), new Dictionary<string, IEnumerable<string>>
                        //         {
                        //             ["desc"] = Description
                        //         });
                        //     }
                        // }
                        // break;
                }
            //}
            // catch (Exception e)
            // {
            //     var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //
            //     Response.Redirect("/"+culture+"/errorpage");
            // }
            
        }
        
    }
}