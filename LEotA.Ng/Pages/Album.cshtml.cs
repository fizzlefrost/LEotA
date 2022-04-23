#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Album>? AlbumList { get; set; }
        

        public IActionResult OnGet()
        {
            //try
            //{
                ViewData.Clear();
                var albumimages = new Dictionary<Album, FileContent?>();
                var albumTotal = _engineClientManager.AlbumGetTotalPages(1).Result;
                AlbumList = _engineClientManager.AlbumGetPaged(null, (albumTotal==1)? 2 : albumTotal, null, null, false);
                if (AlbumList != null)
                    foreach (var album in AlbumList)
                    {
                        FileContent? albumImages = null;
                        var images = _engineClientManager.FileContentGetByMasterIdOne(album.Id);
                        if (images != null)
                        {
                            albumImages = images;
                        }
                        else if (album.MasterId != null)
                        {
                            var masterImages = _engineClientManager.FileContentGetByMasterIdOne((Guid) album.MasterId);
                            if (masterImages != null) albumImages = masterImages;
                        }

                        albumimages.Add(album, albumImages);
                    }

                ViewData.Add("albumTotal",albumTotal);
                ViewData.Add("album",albumimages);
                return Page(); 
            //}
            // catch (Exception e)
            // {
            //     var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //
            //     return Redirect("/" + culture + "/errorpage");
            // }
        }
    }
}