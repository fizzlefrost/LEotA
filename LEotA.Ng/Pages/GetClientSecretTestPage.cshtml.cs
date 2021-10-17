using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    public class GetClientSecretTestPage : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetClientSecretTestPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task OnGet()
        {
            // retrieve to IdentityServer
            var authClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:9001");

            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest()
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "OrdersAPI"
                });
            
            // retrieve to Orders
            var ordersClient = _httpClientFactory.CreateClient();
            
            ordersClient.SetBearerToken(tokenResponse.AccessToken);
            
            var response = await ordersClient.GetAsync("https://localhost:10001/api/about-us/secret");
            var message = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Message"] = response.StatusCode.ToString();
            }
            
            ViewData["Message"] = message;
        }
    }
}