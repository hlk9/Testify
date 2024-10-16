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
               
    }
}
