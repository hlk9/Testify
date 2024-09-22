using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepos;
        public UserController()
        {
            userRepos =new UserRepository();
        }

        [HttpPost("Register-Student")]
        public Task<bool> RegisterStudent([FromBody] User user)
        {
           var a=  userRepos.AddUser(user);

            if (a != null) { 
               
                return Task.FromResult(true);
            }
            return Task.FromResult(false);


        }
    }
}
