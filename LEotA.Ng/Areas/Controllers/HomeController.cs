using Microsoft.AspNetCore.Mvc;

namespace LEotA.Areas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}