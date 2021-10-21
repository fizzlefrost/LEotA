#nullable enable
using System.Collections.Generic;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<EventsModel> _logger;
        private readonly EngineClientManager _engineClientManager;
        public IList<News> _newsList { get; set; }
        public List<NewsColumn> _newsColumnList { get; set; }
        public List<EventViewModel> eventViewModelList { get; set; }

        public IndexModel(EngineClientManager engineClientManager)
        {
            _engineClientManager = engineClientManager;
        }

        public void OnGet()
        {
            _newsList = _engineClientManager.NewsGetPaged(null, 10, null, null, false);
            _newsColumnList = _engineClientManager.NewsColumnGetPaged(null, 10, null, null, false);
            eventViewModelList = new List<EventViewModel>();
            //     foreach (var _event in _newsList)
            //     {
            //         eventViewModelList.Add(new EventViewModel()
            //         {
            //             Event = _event,
            //             Image = _engineClientManager.ImageGetByMasterId(_event.Id)?.Result.Items
            //         });
            //     }
            // }

        }
    }
}