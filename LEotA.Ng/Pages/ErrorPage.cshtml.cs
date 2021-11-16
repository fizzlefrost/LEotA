#nullable enable
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    [Authorize]
    public class ErrorPageModel : PageModel
    {
        public ErrorPageModel()
        {
        }
        
        public async Task OnGet()
        {
            
        }
    }
}