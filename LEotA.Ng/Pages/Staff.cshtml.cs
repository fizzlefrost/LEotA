#nullable enable
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class  StaffModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        [BindProperty(SupportsGet = true)]
        public string SearchRole { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        public StaffModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public void OnGet()
        {
            
        }
    }
}