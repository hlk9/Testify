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

        [HttpGet("get-all-users")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var lstUser = await userRepos.GetAllUsers();
            return Ok(lstUser);
        }

        [HttpPost("create-user")]
        public async Task<ActionResult<User>> CreateAccount(User user)
        {
            var newU = await userRepos.AddUser(user);
            return Ok(newU);
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<User>> UpdateAccount(User user)
        {
            var editUser = await userRepos.UpdateUser(user);
            return Ok(editUser);
        }

        [HttpDelete("delete-user")]
        public async Task<ActionResult<User>> DeleteAccount(Guid id)
        {
            var deleteU = await userRepos.DeleteUser(id);
            return Ok(deleteU);
        }

    }
}
