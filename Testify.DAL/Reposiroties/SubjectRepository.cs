using Microsoft.EntityFrameworkCore;
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

        public async Task<Subject> DeleteSubject(int id)
        {
            try
            {
                var deleteSubject = _context.Subjects.Find(id);

                var objSubject = _context.Subjects.Remove(deleteSubject).Entity;
                await _context.SaveChangesAsync();
                return objSubject;
            }
            catch (Exception)
            {

                return null;
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

    }
}
