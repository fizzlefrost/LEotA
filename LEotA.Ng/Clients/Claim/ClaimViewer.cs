using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace LEotA.Models.Claim
{
    public class ClaimViewer
    {
        public ClaimViewer(string name, IEnumerable<System.Security.Claims.Claim> claims)
        {
            if (claims == null) throw new ArgumentNullException(nameof(claims));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Claims = claims.ToList();
            Token = "N/A";
        }
        
        public ClaimViewer(string name, string tokenJson)
        {
            if (tokenJson == null) throw new ArgumentNullException(nameof(tokenJson));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Claims = ((JwtSecurityToken) new JwtSecurityTokenHandler().ReadToken(tokenJson)).Claims?.ToList();
            Token = tokenJson;
        }

        public List<System.Security.Claims.Claim> Claims { get; set; }

        public string Name { get; set; }
        public string Token { get; set; }
    }
}