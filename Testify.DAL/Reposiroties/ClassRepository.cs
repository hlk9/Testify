using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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

        public async Task<List<Class>> GetAllClassByTeacher(Guid id)

        {
            return await _context.Classes.Where(x => x.TeacherId == id).ToListAsync();
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
                              }).ToListAsync();
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
                              where c.SubjectId == idSubject
                                    && c.Status == 1 
                                    !=_context.ClassExamSchedules.Any(x => x.ClassId == c.Id&&x.ExamScheduleId==scheduleId)
                                    && _context.ExamSchedules.Any(es => es.SubjectId == idSubject)
                                    && _context.ExamSchedules.Any(es => es.SubjectId == idSubject
                                                                        && es.EndTime < scheduleStartTime && es.Id != scheduleId)
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

        public List<User> GetUserInClass(int id)
        {
            var listUser= (from classUser in _context.ClassUsers
             join user in _context.Users
             on classUser.UserId equals user.Id
             where classUser.ClassId == id
             select user
             ).ToList();

            if (listUser == null)
            {
                return null;
            }

            return listUser;
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
        public async Task<ErrorResponse> DeleteClass(int id)
        {
            try
            {
                var objDeleteClass = await _context.Classes.FindAsync(id);

                var hasExam = await _context.ClassExamSchedules.AnyAsync(x => x.ClassId == id);

                if(hasExam)
                {
                    return new ErrorResponse { Success = false, ErrorCode = "PERMISSION_DENIED", Message = "permission_denied" };
                }

                objDeleteClass.Status = 255;
                _context.Classes.Update(objDeleteClass);

                var lstUserInClass = await _context.ClassUsers.Where(x => x.ClassId == id).ToListAsync();

                foreach (var item in lstUserInClass)
                {
                    item.Status = 255;
                    _context.ClassUsers.Update(item);
                }

                await _context.SaveChangesAsync();
                return new ErrorResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { Success = false, ErrorCode = "SERVER_ERROR", Message = ex.Message.ToString() };
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

        public async Task<List<Class>> GetClassesByUserId(Guid userId)
        {
            List<Class> lst = new List<Class>();
            var objUser = _context.Users.Find(userId);

            if (objUser.LevelId == 1 || objUser.LevelId == 2)
            {
                lst = _context.Classes.Where(x => x.Status == 1).ToList();
                return lst;
            }
            else if (objUser.LevelId == 3)
            {
                lst = _context.Classes.Where(x => x.TeacherId == userId && x.Status == 1).ToList();
                return lst;
            }
            else
            {
                return lst;
            }
        }

        public async Task<ScoreDistribution> ScoreDistributionByClass(int ClassId)
        {
            var data = await (from sub in _context.Submissions
                        join es in _context.ExamSchedules on sub.ExamScheduleId equals es.Id
                        join e in _context.Exams on es.ExamId equals e.Id
                        join cles in _context.ClassExamSchedules on es.Id equals cles.ExamScheduleId
                        join clu in _context.ClassUsers on cles.ClassId equals clu.ClassId
                        where (cles.ClassId == ClassId && es.Status == 1 && sub.UserId == clu.UserId)
                        select new
                        {
                            Score = sub.TotalMark,
                            MaxScore = e.MaximmumMark,
                            IsPass = sub.IsPassed,
                        }
                        ).ToListAsync();

            var scores = new List<double>();
            double totalPass = 0;
            double totalFail = 0;

            foreach (var item in data)
            {
                double normalizedScore = item.MaxScore != 10
                    ? (item.Score / item.MaxScore) * 10
                    : item.Score;

                scores.Add(Math.Round(normalizedScore, 2));

                if (item.IsPass)
                {
                    totalPass++;
                }
                else
                {
                    totalFail++;
                }
            }

            var fixedScoreList = Enumerable.Range(0, 11)
                        .Select(i => (double)i)
                        .Union(scores.Distinct().Where(score => score % 1 != 0))
                        .Distinct()
                        .OrderBy(score => score) 
                        .ToList();

            var result = fixedScoreList.Select(score => new ScoreData
            {
                Score = score, 
                CountScore = scores.Count(s => s == score)
            }).ToList();

            double totalCountScore = totalPass + totalFail;

            double percentPass = 0;
            double percentFail = 0;
            if (totalCountScore != 0)
            {
                percentPass = (totalPass / totalCountScore) * 100;

                percentFail = (totalFail / totalCountScore) * 100;
            }

            return new ScoreDistribution
            {
                Data = result,
                Summary = new SummaryData
                {
                    PercentPass = Math.Round(percentPass, 2),
                    PercentFail = Math.Round(percentFail, 2),
                }
            };
        }


        //Là giáo viên thì chỉ hiện những lớp giáo viên này quản lí 
        public async Task<List<ClassWithUser>> GetClassWithUser_OfTeacher(string? textSearch, bool isActive, Guid? teacherID)
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
                                     (!isActive || c.Status == 1 || c.Status == 255) 
                                     && (c.TeacherId == teacherID)
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
                              }).ToListAsync();

            return data;
        }
    }
}
