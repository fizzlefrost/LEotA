﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;

namespace LEotA.Engine.Web.AppStart
{
    /// <summary>
    /// IdentityServer configuration
    /// </summary>
    public class IdentityServerConfig
    {
        /// <summary>
        /// clients want to access resources (aka scopes)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(string oidcEndpoint) =>
            // client credentials client
            new List<Client>
        {
            // resource owner password grant client
            // you can create your own client
            new Client
            {
                ClientId = "microservice1",
                AllowAccessTokensViaBrowser = true,
                IdentityTokenLifetime = 21600,
                AuthorizationCodeLifetime = 21600,
                AccessTokenLifetime = 21600,
                AllowOfflineAccess =  true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 1296000, //in seconds = 15 days
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Address,
                    "leota_client"
                }
            },
            new Client
            {
                ClientId = "leota_client_id",
                ClientSecrets = { new Secret("leota_client_secret".ToSha256()) },
                AllowAccessTokensViaBrowser = true,
                IdentityTokenLifetime = 21600,
                AuthorizationCodeLifetime = 21600,
                AccessTokenLifetime = 21600,
                AllowOfflineAccess =  true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 1296000, //in seconds = 15 days
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Address,
                    "leota_client"
                },
                RedirectUris =
                {
                    oidcEndpoint
                },
                AlwaysIncludeUserClaimsInIdToken = true
            }
        };


        /// <summary>
        /// scopes define the resources in your system
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        /// <summary>
        /// IdentityServer4 API resources
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("api1", "API Default")
        };

        public static IEnumerable<ApiScope> GetAPiScopes()
        {
            yield return new ApiScope("api1");
        }
    }
}
