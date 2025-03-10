﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LEotA.Models.Claim;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace LEotA.Clients.Claim
{
    public class ClaimManager
    {
        public ClaimManager(HttpContext context, ClaimsPrincipal user)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Items = new List<ClaimViewer>();
            var claims = user.Claims.ToList();

            var idTokenJson = context.GetTokenAsync("id_token").GetAwaiter().GetResult();
            var accessTokenJson = context.GetTokenAsync("access_token").GetAwaiter().GetResult();

            AddTokenInfo("Identity Token", idTokenJson);
            AddTokenInfo("Access Token", accessTokenJson);
            AddTokenInfo("User Claims", claims);
        }

        public List<ClaimViewer> Items { get; set; }
        public string? AccessToken { get; set; }

        private void AddTokenInfo(string nameToken, string idTokenJson)
        {
            Items.Add(new ClaimViewer(nameToken, idTokenJson));
        }
        
        private void AddTokenInfo(string nameToken, IEnumerable<System.Security.Claims.Claim> claims)
        {
            Items.Add(new ClaimViewer(nameToken, claims));
        }
    }
}