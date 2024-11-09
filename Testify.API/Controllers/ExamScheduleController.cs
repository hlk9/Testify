using Microsoft.AspNetCore.Mvc;
using Testify.API.DTOs;
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
        public async Task<List<ExamSchedule>> GetScheduleActive()
        {

            return await repos.GetSchedulesActive();
        }


        [HttpGet("Get-Current")]
        public List<ExamSchedule> GetScheduleCurrent()
        {

            return repos.GetScheduleCurrent();
        }

        [HttpGet("Get-InfoBasic")]
        public async Task<List<ExamScheduleDto>> GetInfoBasic()
        {
            List<ExamScheduleDto> listResult = new List<ExamScheduleDto>();
            SubjectRepository subjectRepository = new SubjectRepository();

            var lstSchedule = await repos.GetSchedulesActive();
            var lstSubject = await subjectRepository.GetAllSubject(null, true);
            //var lstExam = 

            foreach (var item in lstSchedule)
            {

                listResult.Add(new ExamScheduleDto { Id = item.Id, Description = item.Description, EndTime = item.EndTime, StartTime = item.StartTime, ExamId = item.ExamId, ExamName = "Không", Status = item.Status, SubjectId = item.SubjectId, SubjectName = lstSubject.FirstOrDefault(x => x.Id == item.SubjectId).Name, Title = item.Title });

            }
            return listResult;

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

        [HttpGet("Get-InTime")]
        public async Task<ExamSchedule> GetInTime(DateTime start, DateTime end, int subjectId)
        {
            return await repos.CheckIsContaintInTime(start, end, subjectId);
        }

        [HttpGet("Get-InTime-NoSubject")]
        public async Task<ExamSchedule> GetInTimeNoSubject(DateTime start, DateTime end)
        {
            return await repos.CheckIsContaintInTimeWithoutSubject(start, end);
        }

        [HttpGet("Get-ById")]
        public async Task<ExamSchedule> GetById(int id)
        {
            return await repos.GetById(id);
        }

        [HttpGet("Get-ExamScheduleTimes-By-ClassUserIdAsync")]
        public async Task<List<ExamSchedule>> GetExamScheduleTimesByClassUserIdAsync(Guid userId)
        {
            return await repos.GetExamScheduleTimesByClassUserIdAsync(userId);
        }
        [HttpGet("Get-All-Schedule-ByStudentId")]
        public async Task<List<ExamScheduleDto>>GetAllScheduleOfStudentById(string studentId)
        {
            List<ExamScheduleDto> listResult = new List<ExamScheduleDto>();
            SubjectRepository subjectRepository = new SubjectRepository();
            UserRepository usrRepos = new UserRepository();
            ClassExamScheduleRepository classExamScheduleRepository = new ClassExamScheduleRepository();
            ClassUserReposiroty classUserReposiroty = new ClassUserReposiroty();
            var lstClass = await classUserReposiroty.GetClassByStudentId(studentId, null, 1);


            var lstSchedule = (from cu in lstClass
                                     join classEx in classExamScheduleRepository.GetAllActive()
                                     on cu.ClassId equals classEx.ClassId
                                     join schedule in repos.GetAll()
                                     on classEx.ExamScheduleId equals schedule.Id
                                     select schedule).ToList();

            var lstSubject = await subjectRepository.GetAllSubject(null, true);

            foreach (var item in lstSchedule)
            {

                listResult.Add(new ExamScheduleDto { Id = item.Id, Description = item.Description, EndTime = item.EndTime, StartTime = item.StartTime, ExamId = item.ExamId, ExamName = "Không", Status = item.Status, SubjectId = item.SubjectId, SubjectName = lstSubject.FirstOrDefault(x => x.Id == item.SubjectId).Name, Title = item.Title });

            }
            return listResult;

        }
    }


}

