using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Lecturer")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        LecturerRepository _repo;
        public LecturerController()
        {
            _repo = new LecturerRepository();
        }

        [HttpGet("Get-All-Lecturer")]
        public async Task<ActionResult<List<User>>> GetlAllLecturer()
        {
            var lstLecturer = await _repo.GetAllLecturer();
            return Ok(lstLecturer);
        }
        [HttpGet("Get-Lecturer-By-Id")]
        public async Task<ActionResult<User>> GetLecturerById(Guid id)
        {
            var Lecturer = await _repo.GetLecturerById(id);
            return Ok(Lecturer);
        }

        [HttpPost("Create-Lecturer")]
        public async Task<ActionResult<User>> Create(User user)
        {
            var createLecturer = await _repo.CreateLecturer(user);
            return Ok(createLecturer);
        }
        [HttpPut("Update-Lecturer")]
        public async Task<ActionResult<User>> Update(User user)
        {
            var updateLecturer = await _repo.UpdateLecturer(user);
            return Ok(updateLecturer);
        }

        [HttpGet("Get-All-Teacher")]
        public async Task<ActionResult<List<User>>> GetlAllTeacher()
        {
            var lstTeacher = await _repo.GetAllTeacher();
            return Ok(lstTeacher);
        }

        [HttpDelete("Delete-Lecturer")]
        public async Task<ActionResult<User>> Delete(Guid id)
        {
            var deleteLecturer = await _repo.DeleteLecturer(id);
            return Ok(deleteLecturer);
        }


    }
}
