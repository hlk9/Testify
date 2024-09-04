using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _repUser;
        public UserController()
        {
            _repUser = new UserRepository();
        }
        [HttpGet("Get-All-User")]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            var lstUser = await _repUser.GetAllUsers();
            return Ok(lstUser);
        }
        [HttpGet("Get-By-Id-User")]
        public async Task<ActionResult<User>> GetByIdUser(int id)
        {
            var objUser = await _repUser.GetUsersId(id);
            return Ok(objUser);
        }
        [HttpPost("Create-User")]
        public async Task<ActionResult<User>> Create(User user)
        {
            var createUser = await _repUser.CreateUser(user);
            return Ok(createUser);
        }
        [HttpPut("Update-User")]
        public async Task<ActionResult<User>> Update(User user)
        {
            var updateUser = await _repUser.UpdateUser(user);
            return Ok(updateUser);
        }
        [HttpDelete("Delete-User")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var deleteUser = await _repUser.DeleteUser(id);
            return Ok(deleteUser);
        }
    }
}
