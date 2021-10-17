using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace LEotA.IdentityServer
{
    public class Configuration
    {
        public static IEnumerable<Client> GetCliens() 
            => new List<Client>() {
            new Client()
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },
                
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "OrdersAPI"
                }
            }
        };

        public static IEnumerable<ApiResource> GetApiResources() 
            => new List<ApiResource>() {
            new ApiResource("OrdersAPI")
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource> {
                new IdentityResources.OpenId()
            };
    }
}