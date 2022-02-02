#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task OnGet()
        {
            //try
            //{
            var staffList = _engineClientManager.StaffGetPaged(null,50,null,null,true);
            var sortedStaffList = staffList.OrderBy(o => o.Position).ToList();
            var staffListWithImage = new Dictionary<Staff, List<FileContent>>();
            foreach (var staff in sortedStaffList)
            {
                var image = _engineClientManager.FileContentGetByMasterId(staff.Id);
                staffListWithImage.Add(staff, image);
            }
            ViewData.Add("staff",staffListWithImage);
            // }
            // catch (Exception e)
            // {
            //     var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //
            //     Response.Redirect("/"+culture+"/errorpage");
            // }
            
        }
    }
}