#nullable enable
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace LEotA.Pages
{
    public class  EventsModel : PageModel
    {
        private readonly ILogger<EventsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public EventsModel(ILogger<EventsModel> logger, EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }
        [BindProperty]
        public IList<Event> events { get; set; }
        public void OnGet()
        {
            events = _engineClientManager.EventGetPaged(null, 10, null, null, false);
        }
    }
}