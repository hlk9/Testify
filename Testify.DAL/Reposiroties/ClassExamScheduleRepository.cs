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

        public List<ClassExamSchedule> GetAllActive()
        {
            return _context.ClassExamSchedules.ToList();
        }
        public List<ClassWithUser> GetClassInSchedule(int scheduleId)
        {
            var data = (from c in _context.Classes
                        join u in _context.Users
                        on c.TeacherId equals u.Id into classUser
                        from cu in classUser.DefaultIfEmpty()
                        join s in _context.Subjects on c.SubjectId equals s.Id into classSubject
                        from cs in classSubject.DefaultIfEmpty()
                        join ces in _context.ClassExamSchedules
                        on c.Id equals ces.ClassId
                        where (cs.Status == 1 && ces.ExamScheduleId == scheduleId)
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
                            Status = c.Status,
                            CurrentCapacity = _context.ClassUsers.Where(x => x.ClassId == c.Id && x.Status == 1).Count()
                        }).ToList();

            return data;
        }

        public bool AddListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            try
            {
                foreach (var c in data)
                {
                    _context.ClassExamSchedules.Add(new ClassExamSchedule { ClassId = c.Id, ExamScheduleId = scheduleId });

                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveListClassToSchedule(List<ClassWithUser> data, int scheduleId)
        {
            try
            {
                foreach (var c in data)
                {

                    var a = _context.ClassExamSchedules.FirstOrDefault(x => x.ClassId == c.Id);

                    if (a != null)
                    {
                        _context.ClassExamSchedules.Remove(a);
                    }

                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
