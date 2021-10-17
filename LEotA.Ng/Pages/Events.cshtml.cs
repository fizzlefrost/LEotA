#nullable enable
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  EventsModel : PageModel
    {
        private readonly ILogger<EventsModel> _logger;
        private readonly EngineClientManager _engineClientManager;

        public EventsModel(ILogger<EventsModel> logger, EngineClientManager engineClientManager)
            //public TestModel( EngineClientManager engineClientManager)
            //public TestModel( EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }

        //public List<AboutUs>? _aboutUsList { get; set; }

        public void OnGet()
        {
            
            //_aboutUsList = _engineClientManager.AboutUsGetPaged(null, 10, null, null, false);
        }
    }
}