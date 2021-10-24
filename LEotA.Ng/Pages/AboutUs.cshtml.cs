#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  AboutUsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;

        public AboutUs ASD { get; set; }
        
        public AboutUsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        
        public void OnGet()
        {
            var aboutUsList = _engineClientManager.AboutUsGetPaged(null, 10, null, null, false);
            var aboutUsReturnList = new List<AboutUs>();
       
            foreach (var aboutUs in aboutUsList)
            {
                var resultItems = _engineClientManager.ImageGetByMasterId(aboutUs.Id)?.Result.Items;
                if (resultItems != null)
                    ViewData.Add("images", new Dictionary<List<Image>, AboutUs>
                    {
                        {resultItems, aboutUs}
                    });
                aboutUsReturnList.Add(aboutUs);
            }
            
            ViewData.Add("aboutUs", aboutUsReturnList);
            
        }
    }
}