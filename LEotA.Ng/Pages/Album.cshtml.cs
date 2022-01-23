#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class  AlbumModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;

        public AlbumModel(EngineClientManager engineClientManager)
            //public TestModel( EngineClientManager engineClientManager)
            //public TestModel( EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public List<Album>? _albumList { get; set; }
        public List<FileContent>? _imageList { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                
                _albumList = _engineClientManager.AlbumGetPaged(null, 10, null, null, false);
                return Page(); 
            }
            catch (Exception e)
            {
                var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

                return Redirect("/" + culture + "/errorpage");
            }
        }
    }
}