﻿using Microsoft.AspNetCore.Mvc;
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
        public bool AddListClassToSchedule([FromBody] List<ClassWithUser> data, int scheduleId)
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
            ExamScheduleRepository examScheduleRepository = new ExamScheduleRepository();
            ClassUserReposiroty classUserReposiroty = new ClassUserReposiroty();
            ClassRepository repoClass = new ClassRepository();
            int scheduleId = req.ScheduleId;

            var oneSchedule = await scheduleRepo.GetById(scheduleId);
            if (oneSchedule == null) return null;
            var scheduleInTime = await examScheduleRepository.CheckIsContaintInTimeWithoutSubject(
                oneSchedule.StartTime, oneSchedule.EndTime);
            if (scheduleInTime == null) return null;

            try
            {
                var listClwU = await classUserReposiroty.GetAll(1);
                var listUsers = await repoUser.GetAllUsers();
                var listStudentPrepare = req.DataList
                    .Join(listClwU, classO => classO.Id, classU => classU.ClassId, (classO, classU) => classU)
                    .Join(listUsers, classU => classU.UserId, usr => usr.Id, (classU, usr) => usr)
                    .ToList();

                var listClassExam = repos.GetAllActive();
                var listStudent_ExistInSchedule = (
                    from classExam in listClassExam
                    join classO in await repoClass.GetAllClass(null, true)
                        on classExam.ClassId equals classO.Id
                    join classU in await classUserReposiroty.GetAll(1)
                        on classO.Id equals classU.ClassId
                    join usr in listUsers
                        on classU.UserId equals usr.Id
                    join examSchedule in scheduleRepo.GetAll()
                        on classExam.ExamScheduleId equals examSchedule.Id
                    where
                     
                        examSchedule.StartTime <= oneSchedule.EndTime &&
                        examSchedule.EndTime >= oneSchedule.StartTime
                    select usr
                ).ToList();

                var commonStudents = listStudentPrepare
                    .Where(s => listStudent_ExistInSchedule.Any(e => e.Id == s.Id))
                    .ToList();

                return commonStudents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

    }
}