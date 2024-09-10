using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Testify.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace Testify.API.TokenHelper
{
    public class AuthHelper
    {
        private readonly IConfiguration _configuration;

        public AuthHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            string a = _configuration["ApplicationSettings:JWT_Secret"];
        }

        public string GenerateJWTToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            };
                var jwtToken = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["ApplicationSettings:TokenExpirationMinutes"])),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])
                            ),
                        SecurityAlgorithms.HmacSha256Signature)
                    );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
