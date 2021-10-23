#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

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

        // public async Task OnPostAsync()
        // {
        //     
        // }
    }
}