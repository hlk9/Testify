using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class ExamScheduleRepository
    {
        TestifyDbContext _context;
        public ExamScheduleRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<ExamSchedule> GetById(int id)
        {
              var a=  await _context.ExamSchedules.FindAsync(id);
            return a;
        }

        public async Task<List<ExamSchedule>> GetSchedulesActive()
        {
            return await _context.ExamSchedules.Where(x => x.Status == 1).ToListAsync();
        }
        public List<ExamSchedule> GetScheduleFuture()
        {
            return _context.ExamSchedules.Where(x => x.Status == 1 && x.StartTime > DateTime.Now).ToList();
        }

        public List<ExamSchedule> GetSchedulePast()
        {
            return _context.ExamSchedules.Where(x => x.Status == 1 && x.EndTime < DateTime.Now).ToList();
        }

        public List<ExamSchedule> GetScheduleCurrent()
        {
            return _context.ExamSchedules.Where(x => x.Status == 1 && x.EndTime > DateTime.Now && x.StartTime >= DateTime.Now).ToList();
        }

        public async Task<bool> AddSchedule(ExamSchedule schedule)
        {

            try
            {
                await _context.ExamSchedules.AddAsync(schedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateSchedule(ExamSchedule schedule)
        {

            try
            {
                var updateOjb = _context.ExamSchedules.Find(schedule.Id);
                if (updateOjb != null)
                {

                    updateOjb.Subject = schedule.Subject;
                    updateOjb.Status = schedule.Status;
                    updateOjb.StartTime = schedule.StartTime;
                    updateOjb.EndTime = schedule.EndTime;
                    updateOjb.Title = schedule.Title;
                    updateOjb.Description = schedule.Description;
                    updateOjb.Exams = schedule.Exams;
                    updateOjb.ExamId = schedule.ExamId;
                    updateOjb.SubjectId = schedule.SubjectId;

                    _context.ExamSchedules.Update(updateOjb);
                    _context.SaveChangesAsync();
                    return true;
                }
                return false;


            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<ExamSchedule> GetAll()
        {
            return _context.ExamSchedules.ToList();
        }

        public bool DeleteSchedule(int id)
        {
            try
            {
                var ojb = _context.ExamSchedules.Find(id);
                if (ojb != null)
                {
                    ojb.Status = 255;
                    _context.ExamSchedules.Update(ojb);
                    _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ExamSchedule> CheckIsContaintInTime(DateTime startDate, DateTime endDate, int subjectId)
        {
            var ojb = await _context.ExamSchedules.FirstOrDefaultAsync(x => x.StartTime <= startDate && endDate <= x.EndTime && x.Status != 255 && x.SubjectId==subjectId || x.StartTime >= startDate && x.EndTime <= endDate && x.Status != 255 && x.SubjectId == subjectId || x.StartTime >= startDate && endDate <= x.EndTime && x.Status != 255 && x.SubjectId == subjectId || startDate <= x.StartTime && endDate >= x.EndTime && x.Status != 255 && x.SubjectId == subjectId);

            if(ojb!= null)
            {
                return ojb;
            }

            return null;
        }

        public async Task<ExamSchedule> CheckIsContaintInTimeWithoutSubject(DateTime startDate, DateTime endDate)
        {
            var ojb = await _context.ExamSchedules.FirstOrDefaultAsync(x => x.StartTime <= startDate && endDate <= x.EndTime && x.Status != 255 || x.StartTime >= startDate && x.EndTime <= endDate && x.Status != 255 || x.StartTime >= startDate && endDate <= x.EndTime && x.Status != 255 || startDate <= x.StartTime && endDate >= x.EndTime && x.Status != 255) ;

            if (ojb != null)
            {
                return ojb;
            }

            return null;
        }

        public Task<List<ExamSchedule>> GetExamScheduleTimesByClassUserIdAsync(Guid userId)
        {
            var result = (from cu in _context.ClassUsers
                          join ces in _context.ClassExamSchedules
                          on cu.ClassId equals ces.ClassId
                          join es in _context.ExamSchedules
                          on ces.ExamScheduleId equals es.Id
                          where cu.UserId == userId && DateTime.Now >= es.StartTime && DateTime.Now <= es.EndTime
                          select es).ToListAsync();
            return result;
        }


    }
}
