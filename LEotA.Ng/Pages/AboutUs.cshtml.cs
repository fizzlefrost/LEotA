#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            var aboutUsList = _engineClientManager.AboutUsGetPaged(null, 100, null, null, false);
            var aboutUsReturnList = new Dictionary<AboutUs, List<FileContent>?>();

            foreach (var aboutUs in aboutUsList)
            {
                var aboutUswithImage = _engineClientManager.FileContentGetByMasterId(aboutUs.Id);
                aboutUsReturnList.Add(aboutUs, aboutUswithImage);
            }
            ViewData.Add("aboutUs", aboutUsReturnList);
        }
    }
}