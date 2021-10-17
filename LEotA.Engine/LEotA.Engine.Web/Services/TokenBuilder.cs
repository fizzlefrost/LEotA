using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LEotA.Engine.Web.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        public string BuildToken(string username)
        {
            var signingKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new(JwtRegisteredClaimNames.Sub, username)
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}