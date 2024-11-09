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
        ClassExamScheduleRepository _repoShede;

        public ClassUserController()
        {
            _repo = new ClassUserReposiroty();
            _repoShede = new ClassExamScheduleRepository();
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

        [HttpPut("Update-Status")]
        public async Task<ActionResult<ClassUser>> UpdateStatus(ClassUser classUser)
        {
            var obj = await _repo.UpdateStatusAsync(classUser);
            return Ok(obj);
        }

        //[HttpPut("Refuse-User")]
        //public async Task<ActionResult<ClassUser>> RefuseUser(ClassUser classUser)
        //{
        //    var obj = await _repo.RefuseUserAsync(classUser);
        //    return Ok(obj);
        //}

        [HttpDelete("Delete-User-In-Class")]
        public async Task<ActionResult<ClassUser>> DeleteUserInClass(Guid id, int classId)
        {
            var obj = await _repo.DeleteUserInClass(id, classId);
            return Ok(obj);
        }
    }
}
