using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        PermissionRepository _repPermission;
        public PermissionController()
        {
            _repPermission = new PermissionRepository();
        }
        [HttpGet("Get-All-Permission")]
        public async Task<ActionResult<List<Permission>>> GetAllPermission()
        {
            var lstPermission = await _repPermission.GetAllPermissions();
            return Ok(lstPermission);
        }
        [HttpGet("Get-By-Id-Permission")]
        public async Task<ActionResult<Permission>> GetByIdPermission(int id)
        {
            var objPermission = await _repPermission.GetPermissionId(id);
            return Ok(objPermission);
        }
        [HttpPost("Create-Permission")]
        public async Task<ActionResult<Permission>> Create(Permission permission) 
        {
            var createPermission = await _repPermission.CreatePermission(permission);
            return Ok(createPermission);
        }
        [HttpPut("Update-Permission")]
        public async Task<ActionResult<Permission>> Update(Permission permission)
        {
            var updatePermission = await _repPermission.UpdatePermission(permission);
            return Ok(updatePermission);
        }
        [HttpDelete("Delete-Permission")]
        public async Task<ActionResult<Permission>> Delete(int id)
        {
            var deletePermission = await _repPermission.DeletePermission(id);
                return Ok(deletePermission);
        }
    }
}
