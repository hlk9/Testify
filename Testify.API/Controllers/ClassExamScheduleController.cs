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

        [HttpPost("Checks-StudentExist-InSchedule")]
        public async Task<List<User>> GetStudentExist(CheckClassScheduleRequest req)
        {
            int scheduleId = req.ScheduleId;
            ExamScheduleRepository examScheduleRepository = new ExamScheduleRepository();
            ClassUserReposiroty classUserReposiroty = new ClassUserReposiroty();
            ClassRepository repoClass = new ClassRepository();
            List<ClassWithUser> data = new List<ClassWithUser>();
            data = req.DataList;
            var tesD = await repoClass.GetAllClass(null, true);


            var oneSchedule = await scheduleRepo.GetById(scheduleId);

            var scheduleInTime =
                examScheduleRepository.CheckIsContaintInTimeWithoutSubject(oneSchedule.StartTime, oneSchedule.EndTime);
            if (scheduleInTime != null)
            {
                try
                {
                    var listStudent_Prepare =
                    (
                     from classO in data
                     join classU in await classUserReposiroty.GetAll(1)
                        on classO.Id equals classU.ClassId into classUsers
                     from classUsr in classUsers.DefaultIfEmpty()
                     join usr in await repoUser.GetAllUsers()
                        on classUsr.UserId equals usr.Id
                     where usr.Id == classUsr.UserId
                     select usr
                    ).ToList();



                    var listSche = scheduleRepo.GetAll();
                    var listClasE = repos.GetAllActive();

                    var listClassExam = (from schedule in scheduleRepo.GetAll()
                                         join classExam in repos.GetAllActive()
                                             on schedule.Id equals classExam.Id
                                         select classExam).ToList();



                    var listStudent_ExistInSchedule =
                         (from classE in listClassExam
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

                    if (listStudent_Prepare.Any(s => listStudent_ExistInSchedule.Contains(s)))
                    {
                        var commonStudents = (from s in listStudent_ExistInSchedule
                                              where listStudent_Prepare.Contains(s)
                                              select s).ToList();
                        return commonStudents;
                    }
                }

                catch (Exception ex)
                {
                    return null;
                }




            }
            return null;

        }
    }
}