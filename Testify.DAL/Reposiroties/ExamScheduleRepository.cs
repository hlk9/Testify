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

        public async Task<List<ExamSchedule>> GetSchedulesActive()
        {
                return await _context.ExamSchedules.Where(x=>x.Status==1).ToListAsync();
        }
        public List<ExamSchedule> GetScheduleFuture()
        {
            return _context.ExamSchedules.Where(x => x.Status == 1 && x.StartTime>DateTime.Now).ToList();
        }

        public List<ExamSchedule> GetSchedulePast()
        {
            return _context.ExamSchedules.Where(x => x.Status == 1 && x.EndTime < DateTime.Now).ToList();
        }

        public List<ExamSchedule> GetScheduleCurrent()
        {
            return _context.ExamSchedules.Where(x => x.Status == 1 && x.EndTime > DateTime.Now && x.StartTime>=DateTime.Now).ToList();
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
                    updateOjb.ExamId = schedule.Id;
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




    }
}
