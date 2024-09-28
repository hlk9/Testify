using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [ApiController]
    [Route("Class")]
    public class ClassController : ControllerBase
    {
        ClassRepository classRepository;
        public ClassController()
        {
            classRepository = new ClassRepository();
        }
        [HttpGet("Get-Classes")]
        public async Task<ActionResult<List<Room>>> GetAll()
        {
            var lstClass = await classRepository.GetAllClass();
            return Ok(lstClass);
        }
        [HttpGet("get-classes-by-id")]
        public async Task<ActionResult<Class>> GetByIdRoom(int id)
        {
            var objClasses = await classRepository.GetByIdClass(id);
            return Ok(objClasses);
        }
        [HttpPost("Add-Class")]
        public async Task<ActionResult<Class>> CreateClass(Class r)
        {
            var addClass = await classRepository.AddClass(r);
            return Ok(addClass);
        }

        [HttpDelete("Delete-Class")]
        public async Task<ActionResult<Class>> DeleteClass(int id)
        {
            var deleteClass = await classRepository.DeleteClass(id);
            return Ok(deleteClass);
        }

        [HttpPut("Update-Class")]
        public async Task<ActionResult<Class>> UpdateClass(Class r)
        {
            var updateClass = await classRepository.UpdateClass(r);
            return Ok(updateClass);
        }
    }
}
