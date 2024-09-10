using Microsoft.AspNetCore.Mvc;
using Testify.API.TokenHelper;
using Testify.DAL.Models;

namespace Testify.API.Controllers
{
    [Route("Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AuthHelper authHelper;
        public TokenController(IConfiguration configuration)
        {
            authHelper = new AuthHelper(configuration);
        }

        [HttpGet("GetToken")]
        public string Index()
        {
            User a = new User();
            a.Id = Guid.NewGuid();
            a.FullName = "hehee";
            return authHelper.GenerateJWTToken(a);
        }
    }
}
