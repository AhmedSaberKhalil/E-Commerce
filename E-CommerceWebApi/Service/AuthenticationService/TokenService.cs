using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_CommerceWebApi.Data;
using E_CommerceWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_CommerceWebApi.Service.AuthenticationService
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {

            var claims = new List<Claim>{};
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret)); 
                                                                                                              
            SigningCredentials signincred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken myToken = new JwtSecurityToken(
                issuer: _jwtSettings.Value.ValidIssuer,      // url web api (Provider)
                audience: _jwtSettings.Value.ValiedAudiance, // url consumer angular
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signincred

                );
            return new JwtSecurityTokenHandler().WriteToken(myToken);
        }
    }
}
