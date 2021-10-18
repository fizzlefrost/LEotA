#nullable enable
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  GalleryModel : PageModel
    {
        private readonly ILogger<GalleryModel> _logger;
        private readonly EngineClientManager _engineClientManager;

        public GalleryModel(ILogger<GalleryModel> logger, EngineClientManager engineClientManager)
            //public TestModel( EngineClientManager engineClientManager)
            //public TestModel( EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }

        public List<Album>? _albumList { get; set; }
        public List<Image>? _imageList { get; set; }

        public void OnGet()
        {

            _albumList = _engineClientManager.AlbumGetPaged(null, 10, null, null, false);
        }
    }
}