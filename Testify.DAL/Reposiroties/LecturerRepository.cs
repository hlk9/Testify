using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class LecturerRepository
    {
        TestifyDbContext _context;
        public LecturerRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<User>> GetAllLecturer(string? textSearch, bool isActive)
        {
            //return await _context.Users.ToListAsync();
            if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == false)
            {
                return await _context.Users.ToListAsync();
            }
            else if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == true)
            {
                return await _context.Users.Where(x => x.Status == 1).ToListAsync();
            }
            else if ((textSearch != null || textSearch != "") && isActive == true)
            {
                return await _context.Users.Where(x => x.UserName.ToLower().Contains(textSearch.Trim().ToLower())
                || x.FullName.ToLower().Contains(textSearch.Trim().ToLower())
                || x.PhoneNumber.ToLower().Contains(textSearch.Trim().ToLower())
                || x.Email.ToLower().Contains(textSearch.Trim().ToLower())
                || x.Address.ToLower().Contains(textSearch.Trim().ToLower()) && x.Status == 1).ToListAsync();
            }
            else
            {
                return await _context.Users.Where(x => x.FullName.ToLower().Contains(textSearch.Trim().ToLower())).ToListAsync();
            }
        }

        public async Task<List<User>> GetAllTeacher()
        {
            return await _context.Users.Where(x => x.LevelId == 3).ToListAsync();
        }

        public async Task<List<User>> GetAllStudent()
        {
            return await _context.Users.Where(x => x.LevelId == 4).ToListAsync();
        }

        public async Task<User> GetLecturerById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> CreateLecturer(User user)
        {
            try
            {
                user.LevelId = 3;
                var addLecturer = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addLecturer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> CreateStudent(User user)
        {
            try
            {
                user.LevelId = 4;
                var addStudent = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addStudent;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateLecturer(User user)
        {
            try
            {
                var updateLecturer = await _context.Users.FindAsync(user.Id);

                updateLecturer.FullName = user.FullName;
                updateLecturer.UserName = user.UserName;
                updateLecturer.DateOfBirth = user.DateOfBirth;
                updateLecturer.PhoneNumber = user.PhoneNumber;
                updateLecturer.Address = user.Address;
                updateLecturer.PasswordHash = user.PasswordHash;
                updateLecturer.AvatarUrl = user.AvatarUrl;
                updateLecturer.LastLogin = user.LastLogin;
                updateLecturer.Email = user.Email;
                updateLecturer.Status = user.Status;
                //updateLecturer.LevelId = 3;
                updateLecturer.LevelId = user.LevelId;


                var objLecturer = _context.Users.Update(updateLecturer).Entity;
                await _context.SaveChangesAsync();
                return objLecturer;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<User> UpdateForgotPass(User user)
        {
            try
            {
                var updateLecturer = await _context.Users.FindAsync(user.Id);

                updateLecturer.PasswordHash = user.PasswordHash;
                updateLecturer.LastLogin = user.LastLogin;

                var objLecturer = _context.Users.Update(updateLecturer).Entity;
                await _context.SaveChangesAsync();
                return objLecturer;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<ErrorResponse> DeleteLecturer(Guid id)
        {
            try
            {
                var objLecturer = await _context.Users.FindAsync(id);

                objLecturer.Status = 255;
                _context.Users.Update(objLecturer);
                await _context.SaveChangesAsync();
                return new ErrorResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { Success = false, ErrorCode = "SERVER_ERROR", Message = ex.Message.ToString() };
            }
        }

        public async Task<List<ScoreStatistics>> GetScore(int subjectId, int examId)
        {


            var data = await (from exs in _context.ExamSchedules.Where(x => x.SubjectId == subjectId && x.ExamId == examId)
                              join subject in _context.Subjects
                              on exs.SubjectId equals subject.Id
                              join ex in _context.Exams
                              on exs.ExamId equals ex.Id
                              join submission in _context.Submissions
                              on exs.Id equals submission.ExamScheduleId
                              select new ScoreStatistics
                              {
                                  UserID = submission.UserId,
                                  SubjectId = subjectId,
                                  SubjectName = subject.Name,
                                  ExamId = ex.Id,
                                  ExamName = ex.Name,
                                  SubmissionId = submission.Id,
                                  Score = submission.TotalMark
                              }).ToListAsync();
            return data;
        }

        public async Task<List<ClassesWithLecturer>> GetScore2(Guid lecId, int classId)
        {


            var objLect = await _context.Users.FindAsync(lecId);

            var data = await (from c in _context.Classes.Where(x => x.TeacherId == lecId && x.Id == classId)
                              join cu in _context.ClassUsers on c.Id equals cu.ClassId
                              join s in _context.Submissions on cu.UserId equals s.UserId
                              select new ClassesWithLecturer
                              {
                                  UserID = lecId,
                                  LecturerName = objLect.UserName,
                                  ClassID = c.Id,
                                  ClassName = c.Name,
                                  SubmissionId = s.Id,
                                  Score = s.TotalMark
                              }
                              ).ToListAsync();
            return data;
        }

        public async Task<List<Class>> GetAllClassByLecturer(Guid lecId)
        {
            return await _context.Classes.Where(x => x.TeacherId == lecId).ToListAsync();
        }

        public async Task<List<ListExamsOfStudent>> GetListExamOfStudent(Guid studentId)
        {


            var data = await (from cluser in _context.ClassUsers.Where(x => x.UserId == studentId)
                              join cla in _context.Classes on cluser.ClassId equals cla.Id
                              join classschedule in _context.ClassExamSchedules on cla.Id equals classschedule.ClassId
                              join exsch in _context.ExamSchedules on classschedule.ExamScheduleId equals exsch.Id
                              join ex in _context.Exams on exsch.ExamId equals ex.Id
                              join sub in _context.Subjects on exsch.SubjectId equals sub.Id


                              select new ListExamsOfStudent
                              {
                                  UserId = studentId,
                                  ExamId = exsch.ExamId,
                                  ExamName = ex.Name,
                                  Duration = ex.Duration,
                                  TotalQuestion = ex.NumberOfQuestions,
                                  Limit = ex.NumberOfRepeat,
                                  ExamScheduleId = exsch.Id,
                                  ExamScheduleName = exsch.Title,
                                  SubjectId = sub.Id,
                                  SubjectName = sub.Name,
                                  ExamScheduleStartTime = exsch.StartTime,
                                  ExamScheduleEndTime = exsch.EndTime,
                                  Status = exsch.Status
                              }
                              ).ToListAsync();

            return data;
        }

        public async Task<int> GetCountStudentByUserId(Guid userId)
        {
            var objUser = _context.Users.Find(userId);

            if (objUser.LevelId == 1 || objUser.LevelId == 2)
            {
                var allStudent = _context.Users.Where(x => x.LevelId == 4 && x.Status == 1).ToList();
                return allStudent.Count;
            }
            else if (objUser.LevelId == 3)
            {
                var data = (from cl in _context.Classes.Where(x => x.TeacherId == userId && x.Status == 1)
                            join clus in _context.ClassUsers on cl.Id equals clus.ClassId
                            where (clus.Status == 1)
                            select(clus.UserId)
                            ).ToList();
                return data.Count;
            }
            else
            {
                return -1;
            }
        }

        public async Task<User> ConfirmEmail(string email)
        {
            var confirm = _context.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();    

            if(confirm != null) {
                return confirm;
            }
            return null;
        }
    }
}
