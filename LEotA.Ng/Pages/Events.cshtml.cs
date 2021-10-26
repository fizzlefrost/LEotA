#nullable enable
using System;
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace LEotA.Pages
{
    public class  EventsModel : PageModel
    {
        private readonly EngineClientManager _engineClientManager;
        public EventsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        
        public void OnGet()
        {
            var eventList = _engineClientManager.EventGetPaged(null, 10, null, null, false);
            var eventReturnList = new List<Event>();
            try
            {
                foreach (var _event in eventList)
                {
                    var resultItems = _engineClientManager.ImageGetByMasterId(_event.Id)?.Result.Items;
                    if (resultItems != null)
                        ViewData.Add("images", new Dictionary<List<Image>, Event>
                        {
                            {resultItems, _event}
                        });
                    eventReturnList.Add(_event);
                }

                ViewData.Add("event", eventReturnList);
            }
            catch 
            {
                
            }
        }
    }
}