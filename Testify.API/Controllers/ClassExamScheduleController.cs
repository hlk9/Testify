using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("ClassExamSchedule")]
    [ApiController]
    public class ClassExamScheduleController : ControllerBase
    {

        ClassExamScheduleRepository repos;
        public ClassExamScheduleController()
        {
            repos = new ClassExamScheduleRepository();
        }

        [HttpGet("Get-Class-ByScheduleId")]
        public List<ClassWithUser> GetClassByScheduleId(int  scheduleId)
        {
            return repos.GetClassInSchedule(scheduleId);
        }

        [HttpPost("Add-ListClassToSchedule")]
        public bool AddListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            return repos.AddListClassToSchedule(data,scheduleId);
        }

        [HttpPost("Remove-ListClassToSchedule")]
        public bool RemoveListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            return repos.RemoveListClassToSchedule(data, scheduleId);
        }

    }
}
