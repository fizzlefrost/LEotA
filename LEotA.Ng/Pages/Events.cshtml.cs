#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class  EventsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [BindProperty(SupportsGet = true)]
        public bool ActiveOnly { get; set; }
        public EventsModel(EngineClientManager engineClientManager, IHttpContextAccessor httpContextAccessor)
        {
            _engineClientManager = engineClientManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public void OnGet()
        {
            var eventList = _engineClientManager.EventGetPaged(null, 10, null, null, false);
                //ViewData.Add("event", eventList);
            if (eventList != null && !ActiveOnly)
            {
                foreach (var _event in eventList)
                {
                    TimeSpan Compare() => (_event.DateTime.CompareTo(DateTime.Now) == 1)
                        ? _event.DateTime - DateTime.Now
                        : new TimeSpan();

                    ViewData.Add(_event.Id.ToString(), Compare());
                }
                ViewData.Add("event", eventList);
            }

            else if (eventList != null && ActiveOnly)
            {
                List<Event> activeEventList = new List<Event>();
                //var activeEventList = eventList;
                foreach (var _event in eventList)
                {
                    if (_event.DateTime.CompareTo(DateTime.Now) == 1)
                    {
                        activeEventList.Add(_event);
                        ViewData.Add(_event.Id.ToString(), _event.DateTime - DateTime.Now);
                    }
                }
                ViewData.Add("event", activeEventList);
            }
        }
    }
}