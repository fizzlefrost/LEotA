#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class  EventsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventsModel(EngineClientManager engineClientManager, IHttpContextAccessor httpContextAccessor)
        {
            _engineClientManager = engineClientManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public void OnGet()
        {
            
                // ViewData.Clear();
                var eventList = _engineClientManager.EventGetPaged(null, 10, null, null, false);
                ViewData.Add("event", eventList);
                var timeList = new TimeSpan();
                if (eventList != null)
                    foreach (var _event in eventList)
                    {
                        TimeSpan Compare() => (_event.DateTime.CompareTo(DateTime.Now) == 1)
                            ? _event.DateTime - DateTime.Now
                            : new TimeSpan();
                        ViewData.Add(_event.Id.ToString(), Compare());
                    }
        }
    }
}