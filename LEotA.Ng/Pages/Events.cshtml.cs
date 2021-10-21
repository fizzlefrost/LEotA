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
        public List<EventViewModel>eventViewModelList { get; set; }
        public EventsModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }
        public void OnGet()
        {
            try
            {
                _eventsList = _engineClientManager.EventGetPaged(null, 10, null, null, false);
                eventViewModelList = new List<EventViewModel>();
                foreach (var _event in _eventsList)
                {
                    eventViewModelList.Add(new EventViewModel()
                    {
                        Event = _event,
                        Image = _engineClientManager.ImageGetByMasterId(_event.Id)?.Result.Items
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("smth gone wrong");
            }
        }
    }
}