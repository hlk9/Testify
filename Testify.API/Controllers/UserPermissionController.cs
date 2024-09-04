using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("UserPermission")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        UserPermissionRepository _repUserPermission;
        public UserPermissionController()
        {
            _repUserPermission = new UserPermissionRepository();
        }
        [HttpGet("Get-All-UserPermission")]
        public async Task<ActionResult<List<UserPermission>>> GetAllUserPermission()
        {
            var lstUserPermission = await _repUserPermission.GetAllUserPermission();
            return Ok(lstUserPermission);
        }
        [HttpGet("Get-By-Id-UserPermission")]
        public async Task<ActionResult<UserPermission>> GetByIdUserPermission(int id)
        {
            var objUserPermission = await _repUserPermission.GetUserPermissionId(id);
            return Ok(objUserPermission);
        }
        [HttpPost("Create-UserPermission")]
        public async Task<ActionResult<UserPermission>> Create(UserPermission userPermission)
        {
            var createUserPermission = await _repUserPermission.CreateUserPermission(userPermission);
            return Ok(createUserPermission);
        }
        [HttpPut("Update-UserPermission")]
        public async Task<ActionResult<UserPermission>> Update(UserPermission userPermission)
        {
            var updateUserPermission = await _repUserPermission.UpdateUserPermission(userPermission);
            return Ok(updateUserPermission);
        }
        [HttpDelete("Delete-UserPermission")]
        public async Task<ActionResult<UserPermission>> Delete(int id)
        {
            var deleteUserPermission = await _repUserPermission.DeleteUserPermission(id);
            return Ok(deleteUserPermission);
        }
    }
}
