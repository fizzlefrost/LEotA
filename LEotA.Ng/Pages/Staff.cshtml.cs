#nullable enable
using System.Collections.Generic;
using System.Linq;
using LEotA.Clients.EngineClient;
using LEotA.Models;
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
            var staffList = _engineClientManager.StaffGetPaged(null,30,null,null,true);
            var sortedStaffList = staffList.OrderBy(o => o.Role).ToList();
            var staffListWithImage = new Dictionary<Staff, List<FileContent>>();
            foreach (var staff in sortedStaffList)
            {
                var image = _engineClientManager.FileContentGetByMasterId(staff.Id);
                
                staffListWithImage.Add(staff, image);
            }
            ViewData.Add("staff",staffListWithImage);
        }
    }
}