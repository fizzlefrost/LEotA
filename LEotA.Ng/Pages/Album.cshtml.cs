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
            //try
            //{
                ViewData.Clear();
                var _albumimages = new Dictionary<Album, List<FileContent>?>();
                var albumTotal = _engineClientManager.AlbumGetTotalPages(1).Result;
                _albumList = _engineClientManager.AlbumGetPaged(null, (albumTotal==1)? 2 : albumTotal, null, null, false);
                foreach (var album in _albumList)
                {
                    var albumImages = new List<FileContent>();
                    var images = _engineClientManager.FileContentGetByMasterId(album.Id);
                    albumImages.AddRange(images);
                    if (album.MasterId != null)
                    {
                        var masterImages = _engineClientManager.FileContentGetByMasterId((Guid) album.MasterId);
                        albumImages.AddRange(masterImages);
                    }
                    _albumimages.Add(album,albumImages);
                }
                ViewData.Add("album",_albumimages);
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