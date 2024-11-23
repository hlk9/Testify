using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class ClassRepository
    {
        private readonly TestifyDbContext _context;
        public ClassRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<Class>> GetAllClass(string? textSearch, bool isActive)
        {
            var query = _context.Classes.AsQueryable();

            if (isActive)
            {
                query = query.Where(x => x.Status == 1 || x.Status == 255);
            }

            if (!string.IsNullOrEmpty(textSearch))
            {
                query = query.Where(c =>
                    c.Name.ToLower().Contains(textSearch.Trim().ToLower()) ||
                    _context.Users.Any(u => u.Id == c.TeacherId && u.FullName.ToLower().Contains(textSearch.Trim().ToLower()))
                );
            }

            return await query.ToListAsync();
        }

        public async Task<List<ClassWithUser>> GetClassWithUser(string? textSearch, bool isActive)
        {
            var filteredClasses = await GetAllClass(textSearch, isActive);

            var data = await (from c in _context.Classes
                              join u in _context.Users
                              on c.TeacherId equals u.Id into classUser
                              from cu in classUser.DefaultIfEmpty()
                              join s in _context.Subjects on c.SubjectId equals s.Id into classSubject
                              from cs in classSubject.DefaultIfEmpty()
                              where (string.IsNullOrEmpty(textSearch) ||
                                     c.Name.ToLower().Contains(textSearch.Trim().ToLower()) ||
                                     cu.FullName.ToLower().Contains(textSearch.Trim().ToLower())) &&
                                     (!isActive || c.Status == 1 || c.Status == 255) // Filter based on isActive status
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
                                  CountUser = _context.ClassUsers.Where(x => x.ClassId == c.Id && x.Status == 1).Count(),
                                  CountConfirm = _context.ClassUsers.Where(x => x.ClassId == c.Id && x.Status == 2).Count(),
                              }).ToListAsync(); // Await the result here

            return data;
        }

        public async Task<List<ClassWithUser>> GetClassWithSubjectId(int idSubject)
        {

            var data = await (from c in _context.Classes
                              join u in _context.Users
                              on c.TeacherId equals u.Id into classUser
                              from cu in classUser.DefaultIfEmpty()
                              join s in _context.Subjects on c.SubjectId equals s.Id into classSubject
                              from cs in classSubject.DefaultIfEmpty()
                              where (c.SubjectId == idSubject && c.Status == 1)
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
                              }).ToListAsync(); // Await the result here

            return data;
        }


        public async Task<List<ClassWithUser>> GetClassWithSubjectIdExcludeInSchedule(int idSubject,int scheduleId)
        {

            // Lấy StartTime của ExamSchedule có Id = scheduleId
            var scheduleStartTime = await _context.ExamSchedules
                .Where(es => es.Id == scheduleId)
                .Select(es => es.StartTime)
                .FirstOrDefaultAsync();

            if (scheduleStartTime == default)
            {
                // Nếu không tìm thấy bản ghi, trả về danh sách rỗng
                return new List<ClassWithUser>();
            }

            // Lấy danh sách ClassWithUser
            var data = await (from c in _context.Classes
                              join u in _context.Users
                              on c.TeacherId equals u.Id into classUser
                              from cu in classUser.DefaultIfEmpty()
                              join s in _context.Subjects on c.SubjectId equals s.Id into classSubject
                              from cs in classSubject.DefaultIfEmpty()
                              where c.SubjectId == idSubject // Lọc theo SubjectId
                                    && c.Status == 1 // Lớp học phải có trạng thái 1 (hoạt động)
                                    !=_context.ClassExamSchedules.Any(x => x.ClassId == c.Id&&x.ExamScheduleId==scheduleId) // Lớp học phải có trong ClassExamSchedules
                                    && _context.ExamSchedules.Any(es => es.SubjectId == idSubject)
                                    && _context.ExamSchedules.Any(es => es.SubjectId == idSubject // Lịch thi thuộc SubjectId
                                                                        && es.EndTime < scheduleStartTime && es.Id != scheduleId) // Điều kiện EndTime < StartTime
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
                              }).ToListAsync();

            return data;
        }


        public async Task<Class> GetByIdClass(int id)
        {
            return await _context.Classes.FindAsync(id);
        }

        public async Task<Class> GetClassByCode(string ClassCode)
        {
            return await _context.Classes.FirstOrDefaultAsync(x => x.ClassCode.ToLower().Equals(ClassCode.ToLower()));
        }

        public async Task<Class> AddClass(Class classes)
        {
            try
            {
                var addClass = _context.Classes.Add(classes).Entity;
                await _context.SaveChangesAsync();
                return addClass;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Class> UpdateClass(Class classes)
        {
            try
            {
                var objUpdateClass = await _context.Classes.FindAsync(classes.Id);

                objUpdateClass.Name = classes.Name;
                objUpdateClass.Capacity = classes.Capacity;
                objUpdateClass.Description = classes.Description;
                objUpdateClass.TeacherId = classes.TeacherId;
                objUpdateClass.SubjectId = classes.SubjectId;

                var updateClass = _context.Classes.Update(objUpdateClass).Entity;
                await _context.SaveChangesAsync();
                return updateClass;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Class> DeleteClass(int id)
        {
            try
            {
                var objDeleteClass = await _context.Classes.FindAsync(id);

                _context.Classes.Remove(objDeleteClass);
                await _context.SaveChangesAsync();
                return objDeleteClass;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Class> UpdateStatusClass(int classId, byte status)
        {
            try
            {
                var objUpdateClass = await _context.Classes.FindAsync(classId);

                objUpdateClass.Status = status;
                var updateClass = _context.Classes.Update(objUpdateClass).Entity;
                await _context.SaveChangesAsync();
                return updateClass;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> GetAllClassByUserId(Guid userId)
        {
            var objUser = _context.Users.Find(userId);

            if (objUser.LevelId == 1 || objUser.LevelId == 2)
            {
                var allClass = _context.Classes.ToList();
                return allClass.Count;
            }
            else if (objUser.LevelId == 3)
            {
                var allClass = _context.Classes.Where(x => x.TeacherId == userId && x.Status == 1).ToList();
                return allClass.Count;
            }
            else if (objUser.LevelId == 4)
            {
                var allClass = _context.ClassUsers.Where(x => x.UserId == userId && x.Status == 1).ToList();
                return allClass.Count;
            }
            else
            {
                return -1;
            }
        }
    }
}
