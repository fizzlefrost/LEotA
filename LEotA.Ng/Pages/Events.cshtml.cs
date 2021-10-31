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
            try {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewData.Add("userId", userId);
                var thisUserEventsList = _engineClientManager.EventParticipantGetByUserIdAsync(new Guid(userId)).Result.Result;
                var eventList = _engineClientManager.EventGetPaged(null, 10, null, null, false);
                var eventReturnList = new List<Event>();
                var isInvolvedList = new List<Guid>();
                    foreach (var _event in eventList)
                    {
                        var resultItems = _engineClientManager.ImageGetByMasterId(_event.Id);
                        if (resultItems != null)
                            ViewData.Add("images", new Dictionary<List<Image>, Event>
                            {
                                {resultItems, _event}
                            });
                        eventReturnList.Add(_event);
                        
                        // is involved check
                        if (thisUserEventsList.Any(e => e.EventId == _event.Id))
                        {
                            isInvolvedList.Add(_event.Id);
                        }
                    }

                ViewData.Add("event", eventReturnList);
                ViewData.Add("isInvolvedList", isInvolvedList);
            }
            catch 
            {
                
            }
        }
    }
}