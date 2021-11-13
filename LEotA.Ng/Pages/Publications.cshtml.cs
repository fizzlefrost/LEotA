#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  PublicationsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        [BindProperty(SupportsGet = true)]
        public string SearchYear { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        
        public PublicationsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public void OnGet()
        {
            ViewData.Clear();
            var publicationsListGetPaged= _engineClientManager.PublicationGetPaged(null, 10, null, null, false);
            var publicationsList = new List<Publication>();
            if (!string.IsNullOrEmpty(SearchYear)){
                if (!string.IsNullOrEmpty(SearchName))
                {
                    publicationsList = publicationsListGetPaged;
                }
                else
                {
                    if (HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name == "ru")
                    {
                        publicationsList = publicationsListGetPaged!.Where(s => s.Text.Russian.Contains(SearchYear))
                            .ToList();
                    }
                    else
                    {
                        publicationsList = publicationsListGetPaged!.Where(s => s.Text.English.Contains(SearchYear))
                            .ToList();
                    }
                }
            }
            else 
            {
                if (!string.IsNullOrEmpty(SearchName))
                {
                    if (HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name == "ru")
                    {
                        publicationsList = publicationsListGetPaged!.Where(s => s.Text.Russian.Contains(SearchYear))
                            .ToList();
                    }
                    else
                    {
                        publicationsList = publicationsListGetPaged!.Where(s => s.Text.English.Contains(SearchYear))
                            .ToList();
                    }
                }
                else
                {
                    publicationsList = publicationsListGetPaged;
                }
            }
            ViewData.Add("Publication",publicationsList);
        }
    }
}