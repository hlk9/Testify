using Microsoft.AspNetCore.Mvc;
using Testify.API.TokenHelper;
using Testify.DAL.Context;
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
        public string Index(Guid id)
        {
            TestifyDbContext context = new TestifyDbContext();
            User a = new User();
            //a = context.Users.Find(Guid.Parse("D1C7E54E-E674-45CA-839D-747B1FF103E7"));
            a = context.Users.Find(id);
            return authHelper.GenerateJWTToken(a).Token;
        }
    }
}
