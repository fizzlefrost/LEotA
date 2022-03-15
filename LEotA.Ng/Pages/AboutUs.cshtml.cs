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
            //try
            //{
                var aboutUsList = _engineClientManager.AboutUsGetPaged(null, 100, null, null, false);
                var aboutUsReturnList = new List<AboutUs>();

                foreach (var aboutUs in aboutUsList)
                {
                    aboutUsReturnList.Add(aboutUs);
                }

                ViewData.Add("aboutUs", aboutUsReturnList);
            //}
            // catch (Exception e)
            // {
            //     var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //     Response.Redirect("/"+culture+"/errorpage");
            // }
        }
    }
}