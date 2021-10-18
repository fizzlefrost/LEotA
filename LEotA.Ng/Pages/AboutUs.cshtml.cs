#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public List<AboutUs>? _aboutUsList { get; set; }
        public Dictionary<string, AboutUs> imgSrcDict = new Dictionary<string, AboutUs>();
        public AboutUsModel(ILogger<AboutUsModel> logger, EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }
        public void OnGet()
        {
            _aboutUsList = _engineClientManager.AboutUsGetPaged(null, 10, null, null, false);
            foreach (var product in _aboutUsList)
            {
                var base64 = Convert.ToBase64String(product.Image);
                imgSrcDict.Add(String.Format("data:image/png;base64,{0}", base64),product);
            }
        }

        public class AboutUsImageViewModel
        {
            public List<AboutUs>? _aboutUsList { get; set; }
            public List<string> imgSrcList { get; set; }
        }
    }
}