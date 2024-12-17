using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        PermissionRepository repository;
        public PermissionController()
        {
            repository = new PermissionRepository();
        }

        [HttpGet("GetAll")]
        public async Task<List<Permission>> GetAll()
        {
            return  await repository.GetAllPermissions();
        }
    }
}
