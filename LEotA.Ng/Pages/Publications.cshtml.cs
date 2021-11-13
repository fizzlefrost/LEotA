#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  PublicationsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        [BindProperty(SupportsGet = true)]
        public string SearchYear { get; set; }
        public PublicationsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public void OnGet()
        {
            var _publicationsList = _engineClientManager.PublicationGetPaged(null, 10, null, null, false);
            if (!string.IsNullOrEmpty(SearchYear)){
                _publicationsList = (List<Publication>?) _publicationsList.Where(s => s.Text.English.Contains(SearchYear));
            }
            ViewData.Add("publication",_publicationsList);
        }
    }
}