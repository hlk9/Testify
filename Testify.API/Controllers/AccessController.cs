using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Reposiroties;
using Testify.API.TokenHelper;
using Testify.DAL.Models;
using Testify.API.DTOs;

namespace Testify.API.Controllers
{
    [Route("Access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        UserRepository UserRepo;
        AuthHelper authHelper;
        public AccessController(IConfiguration configuration)
        {
            authHelper = new AuthHelper(configuration);
        }
      
        [HttpGet("Login")]
        public async Task<UserLoginWithToken> LoginReturnToken(string username, string passwordHash)
        {
            UserRepo = new UserRepository();
            UserLoginWithToken uLT = new UserLoginWithToken();
            User u = await UserRepo.GetByKeyAndPassword(username, passwordHash);
            if (u != null && u.PasswordHash != "-1")
            {
                uLT.Id = u.Id;
                uLT.UserName = u.UserName;
                uLT.FullName = u.FullName;
                uLT.IsLoginSucces = true; 
                uLT.LevelId = u.LevelId;
                 

                uLT.Token = authHelper.GenerateJWTToken(u);
                return uLT;
            }
            uLT.Id = null; 
            uLT.IsLoginSucces = false;
            return uLT;


        }
    }
}
