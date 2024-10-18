using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("ClassUser")]
    [ApiController]
    public class ClassUserController : ControllerBase
    {
        ClassUserReposiroty _repo;

        public ClassUserController()
        {
            _repo = new ClassUserReposiroty();
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<List<ClassUser>>> GetAll(byte Status)
        {
            return Ok(await _repo.GetAll(Status));
        }

        [HttpGet("Get-By-StudentId")]
        public async Task<ActionResult<List<ClassWithClassUser>>> GetAllByStudentId(string StudentId, string? Search, byte Status)
        {
            var all = await _repo.GetClassByStudentId(StudentId, Search, Status);
            return Ok(all);
        }

        [HttpPost("Create-ClassUser")]
        public async Task<ActionResult<ClassUser>> Create(ClassUser classUser)
        {
            var obj = await _repo.Create(classUser);
            return Ok(obj);
        }
    }
}
