using Microsoft.AspNetCore.Mvc;
using Testify.API.TokenHelper;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("ExamSchedule")]
    [ApiController]
    public class ExamScheduleController : ControllerBase
    {
        ExamScheduleRepository repos;

        public ExamScheduleController()
        {
            repos = new ExamScheduleRepository();
        }
        [HttpPost("Create")]
        public async Task<bool> Add(ExamSchedule schedule)
        {
            return await repos.AddSchedule(schedule);
        }
        [HttpGet("Get-All")]
        public List<ExamSchedule> GetAll()
        {
            return repos.GetAll();
        }

        [HttpGet("Get-Future")]
        public List<ExamSchedule> GetScheduleFuture()
        {

            return repos.GetScheduleFuture();
        }

        [HttpGet("Get-Past")]
        public List<ExamSchedule> GetSchedulePast()
        {

            return repos.GetSchedulePast();
        }

        [HttpGet("Get-Active")]
        public List<ExamSchedule> GetScheduleActive()
        {

            return repos.GetSchedulesActive();
        }


        [HttpGet("Get-Current")]
        public List<ExamSchedule> GetScheduleCurrent()
        {

            return repos.GetScheduleCurrent();
        }

        [HttpPut("Update")]
        public bool UpdateSchedule(ExamSchedule schedule)
        {

            return repos.UpdateSchedule(schedule);
        }

        [HttpDelete("Delete")]
        public bool DeleteSchedule(int id)
        {
            return repos.DeleteSchedule(id);
        }
    }


}

