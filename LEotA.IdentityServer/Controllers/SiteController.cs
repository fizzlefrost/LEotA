using Microsoft.AspNetCore.Mvc;

namespace LEotA.IdentityServer.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}