// #nullable enable
// using System.Collections.Generic;
// using System.IO;
// using System.Threading.Tasks;
// using LEotA.Clients.EngineClient;
// using LEotA.Models;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using System.IO;
// using System.Threading.Tasks;
//
// namespace LEotA.Pages
// {
//     public class SecretModel : PageModel
//     {
//         private readonly ILogger<EventsModel> _logger;
//         private readonly EngineClientManager _engineClientManager;
//         
//         public IList<News> _newsList { get; set; }
//         public List<NewsColumn> _newsColumnList { get; set; }
//         public List<EventViewModel> eventViewModelList { get; set; }
//
//         public SecretModel(EngineClientManager engineClientManager)
//         {
//             _engineClientManager = engineClientManager;
//         }
//
//         public void OnGet()
//         {
//             
//             var model = new ClaimManager(HttpContext, User);
//
//             var client = _httpClientFactory.CreateClient();
//
//             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.AccessToken);
//
//             var stringAsync = await client.GetAsync("https://localhost:5003/Secret");
//             
//             ViewData.Add("secret_message", stringAsync);
//         }
//     }
// }