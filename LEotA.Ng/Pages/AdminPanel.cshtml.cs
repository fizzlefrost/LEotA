#nullable enable
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class AdminPanelModel : PageModel
    {
        private readonly ILogger<AdminPanelModel> _logger;
        private readonly EngineClientManager _engineClientManager;

        public AdminPanelModel(ILogger<AdminPanelModel> logger, EngineClientManager engineClientManager)
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