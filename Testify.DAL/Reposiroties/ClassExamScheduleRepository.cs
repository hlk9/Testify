using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class ClassExamScheduleRepository
    {
        TestifyDbContext _context;
        public ClassExamScheduleRepository()
        {
            _context = new TestifyDbContext();
        }
        public List<ClassWithUser> GetClassInSchedule(int scheduleId)
        {
            var data =  (from c in _context.Classes
                             join u in _context.Users
                             on c.TeacherId equals u.Id into classUser
                             from cu in classUser.DefaultIfEmpty()
                             join s in _context.Subjects on c.SubjectId equals s.Id into classSubject
                             from cs in classSubject.DefaultIfEmpty()
                            from schedule in _context.ClassExamSchedules
                            where(schedule.Id== scheduleId && c.Status==1)
                             select new ClassWithUser
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 ClassCode = c.ClassCode,
                                 Description = c.Description,
                                 Capacity = c.Capacity,
                                 TeacherId = c.TeacherId,
                                 FullName = cu.FullName,
                                 SubjectId = c.SubjectId,
                                 SubjectName = cs.Name,
                                 Status = c.Status
                             }).ToList(); // Await the result here

            return data;
        }
    }
}
