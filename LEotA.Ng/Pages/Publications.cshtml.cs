﻿#nullable enable
using System.Collections.Generic;
using System.Linq;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class  PublicationsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        [BindProperty(SupportsGet = true)]
        public int SearchYear { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        
        public PublicationsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public void OnGet()
        {
            //try
            //{
                var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
                ViewData.Clear();
                var publicationsListGetPaged= _engineClientManager.PublicationGetPaged(null, 50, null, null, false);
                var publicationsList = new List<Publication>();
                var publicationsPdfList = new Dictionary<Publication, List<FileContent>>();
                if (SearchYear != 0){
                    if (!string.IsNullOrEmpty(SearchName))
                    {
                        if (culture == "ru")
                        {
                            publicationsList = publicationsListGetPaged
                                .Where(s => s.Name.Russian.Contains(SearchName)).Where(s => s.Time.Year.Equals(SearchYear)).ToList();
                        }
                        else
                        {
                            publicationsList = publicationsListGetPaged
                                .Where(s => s.Name.English.Contains(SearchName)).Where(s => s.Time.Year.Equals(SearchYear)).ToList();
                        }
                    }
                    else
                    {
                        publicationsList = publicationsListGetPaged.Where(s => s.Time.Year.Equals(SearchYear)).ToList();
                    }
                }
                else 
                {
                    if (!string.IsNullOrEmpty(SearchName))
                    {
                        if (culture == "ru")
                        {
                            publicationsList = publicationsListGetPaged!.Where(s => s.Text.Russian.Contains(SearchName))
                                .ToList();
                        }
                        else
                        {
                            publicationsList = publicationsListGetPaged!.Where(s => s.Text.English.Contains(SearchName))
                                .ToList();
                        }
                    }
                    else
                    {
                        publicationsList = publicationsListGetPaged;
                    }
                }
                foreach (var publication in publicationsList)
                {
                    var pdf = _engineClientManager.FileContentGetByMasterId(publication.Id);
                    if (pdf != null)
                    {
                        publicationsPdfList.Add(publication, pdf);
                    }
                }
                ViewData.Add("Publication",publicationsPdfList);
            // }
            // catch (Exception e)
            // {
            //     var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //
            //     Response.Redirect("/"+culture+"/errorpage");
            // }
            
        }
    }
}