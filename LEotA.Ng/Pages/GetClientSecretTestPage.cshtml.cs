using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class GetClientSecretTestPage : PageModel
    {
        public void OnGet()
        {
            ViewData["Message"] = "asdasd";
        }
    }
}