using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

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
        public async Task<ActionResult<List<ClassWithUser>>> GetAll(string? keyword, bool isActive)
        {
            var lstClass = await classRepository.GetClassWithUser(keyword, isActive);
            return Ok(lstClass);
        }

        [HttpGet("Get-ClassList")]
        public async Task<ActionResult<List<Class>>> GetList(string? keyword, bool isActive)
        {
            var lstClass = await classRepository.GetAllClass(keyword, isActive);
            return Ok(lstClass);
        }

        [HttpGet("Get-Classes-BySubjectIdExcludeInSchedule")]
        public async Task<ActionResult<List<ClassWithUser>>> GetAll(int subjectId)
        {
            var lstClass = await classRepository.GetClassWithSubjectIdExcludeInSchedule(subjectId);
            return Ok(lstClass);
        }

        [HttpGet("get-classes-by-id")]
        public async Task<ActionResult<Class>> GetByIdRoom(int id)
        {
            var objClasses = await classRepository.GetByIdClass(id);
            return Ok(objClasses);
        }

        [HttpGet("Get-Class-By-ClassCode")]
        public async Task<ActionResult<Class>> GetClassByCode(string ClassCode)
        {
            var obj = await classRepository.GetClassByCode(ClassCode);
            return Ok(obj);
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
        public async Task<ActionResult<Class>> UpdateClass(Class c)
        {
            var updateClass = await classRepository.UpdateClass(c);
            return Ok(updateClass);
        }

        [HttpPut("Update-Status")]
        public async Task<ActionResult<Class>> UpdateStatus(int classId, byte status)
        {
            var updateStatus = await classRepository.UpdateStatusClass(classId, status);
            return Ok(updateStatus);
        }

        [HttpGet("Get-Count-Class-By-UserId")]
        public async Task<ActionResult<int>> GetCountClass(Guid userId)
        {
            var count = await classRepository.GetAllClassByUserId(userId);

            return Ok(count);
        }
    }
}
