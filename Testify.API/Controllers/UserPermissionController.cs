using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("UserPermission")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        UserPermissionRepository userPermissionRepository;

        public UserPermissionController()
        {
            userPermissionRepository = new UserPermissionRepository();
        }

        [HttpPost("Create-UserPermission")]
        public async Task<bool> Create(UserPermission userPermission)
        {
            var obj =  userPermissionRepository.AddUserPermission(userPermission);
            return obj;
        }

        [HttpDelete("Delete-ListUserPermission")]
        public async Task<bool> RemoveListUP([FromBody] List<int> ids)
        {
            var stats =  userPermissionRepository.RemoveListUserPermission(ids);
            return stats;
        }

        [HttpDelete("Delete-UserPermission")]
        public async Task<bool> Delete(int id)
        {
            var obj =  userPermissionRepository.DeleteUserPermission(id);
            return obj;
        }

        [HttpGet("Get-PermissionByUserId")]
        public async Task<List<UserPermission>> GetPermissionByUserId(Guid id)
        {
            return await userPermissionRepository.GetByUserIdPermission(id);
        }

        [HttpGet("Check-PermissionByUserIdAndName")]
        public async Task<bool> CheckPermission(Guid userId, string permission)
        {
            return await userPermissionRepository.HasPermissionAsync(userId, permission);
        }

        [HttpPost("Add-ListUserPermission")]
        public bool AddListUserPermission(List<UserPermission> listUp)
        {
            var stats =   userPermissionRepository.AddListUserPermission(listUp);
            return stats;
        }
    }
}
