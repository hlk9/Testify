using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class SubjectRepository
    {
        TestifyDbContext _context;
        public SubjectRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<Subject>> GetAllSubject(string? textSearch, bool isActive)
        {
            if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == false)
            {
                return await _context.Subjects.ToListAsync();
            }
            else if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == true)
            {
                return await _context.Subjects.Where(x => x.Status == 1).ToListAsync();
            }
            else if ((textSearch != null || textSearch != "") && isActive == true)
            {
                return await _context.Subjects.Where(x => x.Name.ToLower().Contains(textSearch.Trim().ToLower()) && x.Status == 1).ToListAsync();
            }
            else
            {
                return await _context.Subjects.Where(x => x.Name.ToLower().Contains(textSearch.Trim().ToLower())).ToListAsync();
            }
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject> CreateSubject(Subject sub)
        {
            try
            {
                var addSubject = _context.Subjects.Add(sub).Entity;
                await _context.SaveChangesAsync();
                return addSubject;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Subject> UpdateSubject(Subject sub)
        {
            try
            {
                var updateSubject = _context.Subjects.Find(sub.Id);

                updateSubject.Name = sub.Name;
                updateSubject.Description = sub.Description;
                updateSubject.Status = sub.Status;
                var objSubject = _context.Subjects.Update(updateSubject).Entity;
                await _context.SaveChangesAsync();
                return objSubject;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<ErrorResponse> DeleteSubject(int id)
        {
            try
            {
                var deleteSubject = _context.Subjects.Find(id);

                var isExam = await _context.Exams.AnyAsync(x => x.SubjectId == id);
                var isExamSchedule = await _context.ExamSchedules.AnyAsync(x => x.SubjectId == id);
                var isClass = await _context.Classes.AnyAsync(x => x.SubjectId == id);
                var isQuestion = await _context.Questions.AnyAsync(x => x.SubjectId == id);

                if (isExam || isExamSchedule || isClass || isQuestion)
                {
                    return new ErrorResponse { Success = false, ErrorCode = "PERMISSION_DENIED", Message = "permission_denied" };
                }
                deleteSubject.Status = 255;
                var objSubject = _context.Subjects.Update(deleteSubject);
                await _context.SaveChangesAsync();
                return new ErrorResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { Success = false, ErrorCode = "SERVER_ERROR", Message = ex.Message.ToString() };
            }
        }

        public async Task<int> GetCountSubjectByUserId(Guid userId)
        {
            var objUser = _context.Users.Find(userId);

            if (objUser.LevelId == 1 || objUser.LevelId == 2)
            {
                var allSubject = _context.Subjects.ToList();
                return allSubject.Count;
            }

            return -1;
        }
        public async Task<ScoreDistribution> ScoreDistributionBySubject(int subjectId)
        {
            var data = await (from submission in _context.Submissions
                              join examschedule in _context.ExamSchedules on submission.ExamScheduleId equals examschedule.Id
                              join exam in _context.Exams on examschedule.ExamId equals exam.Id
                              join subject in _context.Subjects on examschedule.SubjectId equals subject.Id
                              where (exam.Status == 1 && subject.Id == subjectId && submission.Status == true && examschedule.Status == 1)
                              select new
                              {
                                  Score = submission.TotalMark,
                                  MaxScore = exam.MaximmumMark,
                                  IsPass = submission.IsPassed,
                              }).ToListAsync();


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

        public async Task<List<SubmissionViewModel>> GetSubmissionDetails(int? subjectId, string? textSearch, Guid? usersID, int? classId, string? startTime, string? endTime)
        {

            var lstne = await (from submis in _context.Submissions
                               join u in _context.Users on submis.UserId equals u.Id
                               join examsche in _context.ExamSchedules on submis.ExamScheduleId equals examsche.Id
                               join sub in _context.Subjects on examsche.SubjectId equals sub.Id
                               join e in _context.Exams on examsche.ExamId equals e.Id
                               join ces in _context.ClassExamSchedules on examsche.Id equals ces.ExamScheduleId
                               join c in _context.Classes on ces.ClassId equals c.Id
                               join cu in _context.ClassUsers on c.Id equals cu.ClassId
                               where (cu.UserId == submis.UserId && (string.IsNullOrEmpty(textSearch) ||
                                      c.Name.ToLower().Contains(textSearch.Trim().ToLower()) ||
                                      u.FullName.ToLower().Contains(textSearch.Trim().ToLower()) ||
                                      sub.Name.ToLower().Contains(textSearch.Trim().ToLower()) ||
                                      e.Name.ToLower().Contains(textSearch.Trim().ToLower())))
                               select new SubmissionViewModel
                               {
                                   ID = submis.Id,
                                   SubmitTime = submis.SubmitTime,
                                   Note = submis.Note,
                                   UserName = u.FullName,
                                   TeacherId = c.TeacherId,
                                   Email = u.Email,
                                   ExamName = e.Name,
                                   SubjectId = sub.Id,
                                   SubjectName = sub.Name,
                                   ClassId = c.Id,
                                   ClassName = c.Name,
                                   Status = submis.Status
                               }).ToListAsync();

            User user = new User();

            if (usersID != null)
            {
                user = _context.Users.Find(usersID);
            };

            if (user.LevelId == 1 || user.LevelId == 2)
            {
                if (subjectId == -1)
                {
                    if (classId == -1)
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime)).ToList();
                    }
                    else
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && a.ClassId == classId).ToList();
                    }
                }
                else
                {
                    if (classId == -1)
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && a.SubjectId == subjectId).ToList();
                    }
                    else
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && a.SubjectId == subjectId && a.ClassId == classId).ToList();
                    }
                }
            }
            else if (user.LevelId == 3)
            {
                if (subjectId == -1)
                {
                    if (classId == -1)
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && a.TeacherId == user.Id).ToList();
                    }
                    else
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && classId == a.ClassId && a.TeacherId == user.Id).ToList();
                    }
                }
                else
                {
                    if (classId == -1)
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && subjectId == a.SubjectId && a.TeacherId == user.Id).ToList();
                    }
                    else
                    {
                        return lstne.Where(a => a.SubmitTime <= Convert.ToDateTime(endTime) && a.SubmitTime >= Convert.ToDateTime(startTime) && subjectId == a.SubjectId && classId == a.ClassId && a.TeacherId == user.Id).ToList();
                    }
                }
            }
            return lstne;
        }
    }
}
