#nullable enable
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  ProjectsModel : PageModel
    {
        private readonly ILogger<ProjectsModel> _logger;
        private readonly EngineClientManager _engineClientManager;

        public ProjectsModel(ILogger<ProjectsModel> logger, EngineClientManager engineClientManager)
        {
            _logger = logger;
            _engineClientManager = engineClientManager;
        }
        
        public void OnGet()
        {
            ViewData.Add("TestProject", new Project()
            {
                EmbedLink = "ykt.ru",
                
            });
        }
    }
}