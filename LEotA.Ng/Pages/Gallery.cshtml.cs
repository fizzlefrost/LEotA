#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class  GalleryModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;

        public GalleryModel(EngineClientManager engineClientManager) {
            _engineClientManager = engineClientManager;
        }
        public Dictionary<Guid, int> IntAssign = new Dictionary<Guid, int>();

        public IActionResult OnGet(Guid id)
        {
            ViewData.Clear();
            var album = _engineClientManager.AlbumGetById(id)?.Result;
            var images = _engineClientManager.FileContentGetByMasterId(id);
            ViewData.Add("albumName", album);
            ViewData.Add("albumId", images);
            var j = 1;
            foreach (var x in images)
            {
                IntAssign.Add(x.Id,j);
                j++;
            }
            return Page(); 
        }
    }
}