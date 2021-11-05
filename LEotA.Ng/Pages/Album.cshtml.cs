#nullable enable
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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

             _albumList = _engineClientManager.AlbumGetPaged(null, 10, null, null, false);
             return Page(); 
        }
    }
}