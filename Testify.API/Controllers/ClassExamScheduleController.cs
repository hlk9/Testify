using Microsoft.AspNetCore.Mvc;
using Testify.API.DTOs;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("ClassExamSchedule")]
    [ApiController]
    public class ClassExamScheduleController : ControllerBase
    {
        ClassExamScheduleRepository repos;
        ExamScheduleRepository scheduleRepo;
        UserRepository repoUser;

        public ClassExamScheduleController()
        {
            repos = new ClassExamScheduleRepository();
            scheduleRepo = new ExamScheduleRepository();
            repoUser = new UserRepository();
        }

        [HttpGet("Get-Class-ByScheduleId")]
        public List<ClassWithUser> GetClassByScheduleId(int scheduleId)
        {
            return repos.GetClassInSchedule(scheduleId);
        }

        [HttpPost("Add-ListClassToSchedule")]
        public bool AddListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            return repos.AddListClassToSchedule(data, scheduleId);
        }

        [HttpPost("Remove-ListClassToSchedule")]
        public bool RemoveListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            return repos.RemoveListClassToSchedule(data, scheduleId);
        }

        [HttpPost("Get-StudentExist-InSchedule")]
        public async Task<List<ClassWithUser>> GetStudentExist(CheckClassScheduleRequest req)
        {
            int scheduleId = req.ScheduleId;
            ExamScheduleRepository examScheduleRepository = new ExamScheduleRepository();
            ClassUserReposiroty classUserReposiroty = new ClassUserReposiroty();
            List<ClassWithUser> data = new List<ClassWithUser>();
            data = req.DataList;
            var listData_1 =
                (from schedule in scheduleRepo.GetAll()
                    join classExam in repos.GetAllActive()
                        on schedule.Id equals classExam.ExamScheduleId
                    where schedule.Id == scheduleId
                    select classExam
                ).ToList();
            ClassRepository repoClass = new ClassRepository();
            List<Class> listClass = new List<Class>();
            foreach (var item in listData_1)
            {
                listClass.Add(await repoClass.GetByIdClass(item.ClassId));
            }

            var oneSchedule = await scheduleRepo.GetById(scheduleId);

            var scheduleInTime =
                examScheduleRepository.CheckIsContaintInTime(oneSchedule.StartTime, oneSchedule.EndTime);
            if (scheduleInTime != null)
            {
                var listStudent_ExistInSchedule =
                    (from schedule in scheduleRepo.GetAll()
                        join classExam in repos.GetAllActive()
                            on schedule.Id equals classExam.ExamScheduleId into classExams
                        from classE in classExams.DefaultIfEmpty()
                        join classOrigin in await repoClass.GetAllClass(null, true)
                            on classE.ClassId equals classOrigin.Id into classOrigins
                        from classO in classOrigins.DefaultIfEmpty()
                        join classU in await classUserReposiroty.GetAll(1)
                            on classO.Id equals classU.ClassId into classUsers
                        from classUsr in classUsers.DefaultIfEmpty()
                        join usr in await repoUser.GetAllUsers()
                            on classUsr.UserId equals usr.Id
                        where usr.Id == classUsr.UserId
                        select usr
                    ).ToList();
            }


            return null;
        }
    }
}