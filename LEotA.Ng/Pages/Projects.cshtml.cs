#nullable enable
using LEotA.Clients.EngineClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class  ProjectsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;

        public ProjectsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        
        public void OnGet()
        {
            //try
            //{
                var _projectList = _engineClientManager.ProjectGetPaged(null, 10, null, null, false);
                ViewData.Add("projects",_projectList);
                
            //}
            // catch (Exception e)
            // {
            //     var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //
            //     Response.Redirect("/"+culture+"/errorpage");
            // }
        }
    }
}