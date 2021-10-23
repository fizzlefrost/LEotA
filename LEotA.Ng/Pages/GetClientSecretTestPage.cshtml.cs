using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEotA.Pages
{
    [Authorize]
    public class GetClientSecretTestPage : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthorizationService _authorizationService;

        public GetClientSecretTestPage(IHttpClientFactory httpClientFactory, IAuthorizationService authorizationService)
        {
            _httpClientFactory = httpClientFactory;
            _authorizationService = authorizationService;
        }
        
        public async Task<IActionResult> OnGet()
        {
            // retrieve to IdentityServer
            // var authClient = _httpClientFactory.CreateClient();
            // var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001");
            //
            // var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(
            //     new ClientCredentialsTokenRequest()
            //     {
            //         Address = discoveryDocument.TokenEndpoint,
            //         ClientId = "client_id",
            //         ClientSecret = "client_secret",
            //         Scope = "OrdersAPI"
            //     });
            //
            // // retrieve to Orders
            // var ordersClient = _httpClientFactory.CreateClient();
            //
            // ordersClient.SetBearerToken(tokenResponse.AccessToken);
            //
            // var response = await ordersClient.GetAsync("https://localhost:10001/api/about-us/secret");
            // var message = await response.Content.ReadAsStringAsync();
            //
            // if (!response.IsSuccessStatusCode)
            // {
            //     ViewData["Message"] = response.StatusCode.ToString();
            // }
            
            // This is your policy you've defined in Startup.cs
            var policyCheck = await _authorizationService.AuthorizeAsync(User, "oidc");

            // Check the result, and return a forbid result to the user if failed.
            if (!policyCheck.Succeeded) {
                return Forbid();
            }

            // ...

            return Page(); // Or RedirectToPage etc

            var message = "amogus";
            
            ViewData["Message"] = message;
        }
    }
}