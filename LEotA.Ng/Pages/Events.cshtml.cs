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
        private readonly ILogger<EventsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public IList<Event>? _eventsList { get; set; }
        public Dictionary<List<Image>, Event> _eventImages { get; set; }
        public EventsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        public void OnGet()
        {
            try
            {
                _eventsList = _engineClientManager.EventGetPaged(null, 10, null, null, false);
                foreach (var _event in _eventsList)
                {
                    _eventImages.Add(_engineClientManager.ImageGetByMasterId(_event.Id).Result.Items, _event);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("smth gone wrong");
            }
        }
    }
}