using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Testify.DAL.Models;
using Microsoft.Extensions.Configuration;
using Testify.API.Utilities;
using Testify.DAL.Reposiroties;

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

        public TokenUserModel GenerateJWTToken(User user)
        {
            var regDate = DateTime.UtcNow;
            var expried = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["ApplicationSettings:TokenExpirationMinutes"]));

            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            };
                var jwtToken = new JwtSecurityToken(
                    claims: claims,
                    notBefore: regDate,
                    expires: expried,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])
                            ),
                        SecurityAlgorithms.HmacSha256Signature)
                    );

            TokenUserModel tokenUserModel = new TokenUserModel();
            tokenUserModel.UserId = user.Id;
            tokenUserModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            tokenUserModel.Expried = expried;
            tokenUserModel.RegisterDate = regDate;

            return tokenUserModel;
        }

        public bool CheckTokenExpried(Guid uid,string token)
        {
            RefreshTokenRepository repo = new RefreshTokenRepository();

            return repo.CheckTokenExpried(uid, token);

        }

        

    }
}
