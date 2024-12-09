using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class SubmissionReposiroty
    {
        TestifyDbContext _context;

        public SubmissionReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<Submission>> GetAll()
        {
            return await _context.Submissions.ToListAsync();
        }

        public async Task<Submission> GetById(int id)
        {
            return await _context.Submissions.FindAsync(id);
        }

        public async Task<Submission> Create(Submission submission)
        {
            try
            {
                var objSubmission = _context.Submissions.Add(submission).Entity;
                await _context.SaveChangesAsync();
                return objSubmission;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Submission>> GetHistory(Guid userId, int examscheduleId)
        {
            var data = await _context.Submissions.Where(x => x.UserId == userId && x.ExamScheduleId == examscheduleId).ToListAsync();
            return data;
        }
        public async Task<int> CheckNumberOfSubmit(Guid userId, int examscheduleId)
        {
            try
            {
                var numberrepeat = await _context.Submissions.Where(x => x.UserId == userId && x.ExamScheduleId == examscheduleId).ToListAsync();
                return numberrepeat.Count();
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public async Task<List<SubmissionWithName>> GetAllSubmittedByUser(Guid userId)
        {
            var data = await (from submit in _context.Submissions
                              join u in _context.Users
                              on submit.UserId equals u.Id
                              join exD in _context.ExamDetails
                              on submit.ExamDetailId equals exD.Id
                              join ex in _context.Exams
                              on exD.ExamId equals ex.Id
                              join sub in _context.Subjects
                              on ex.SubjectId equals sub.Id
                              where submit.UserId == userId
                              select new SubmissionWithName
                              {
                                  Id = submit.Id,
                                  Name = ex.Name,
                                  SubjectName = sub.Name,
                                  UserId = userId,
                                  SubmitTime = submit.SubmitTime,
                                  TimeTaken = submit.TimeTaken,
                                  TotalMark = submit.TotalMark,
                                  IsPassed = submit.IsPassed,
                                  Note = submit.Note,
                                  SubmissionId = submit.Id,
                                  ExamDetailId = exD.Id


                              }).ToListAsync();

            return data;
        }

        public async Task<List<Achievenment>> GetAllAchievenment(Guid userId)
        {
            var data = await (from sub in _context.Submissions
                              join es in _context.ExamSchedules on sub.ExamScheduleId equals es.Id
                              join s in _context.Subjects on es.SubjectId equals s.Id
                              join ces in _context.ClassExamSchedules on es.Id equals ces.ExamScheduleId
                              join c in _context.Classes on ces.ClassId equals c.Id
                              join cu in _context.ClassUsers on c.Id equals cu.ClassId
                              where cu.UserId == userId && sub.UserId == userId
                              select new Achievenment
                              {
                                  ClassId = c.Id,
                                  ClassName = c.Name,
                                  SubjectName = s.Name,
                                  AvgScore = sub.TotalMark
                              }
                              ).ToListAsync();

            var groupedData = data
                            .GroupBy(d => new { d.ClassId, d.ClassName })
                            .Select(g => new Achievenment
                            {
                                ClassId = g.Key.ClassId,
                                ClassName = g.Key.ClassName,
                                SubjectName = string.Join(", ", g.Select(x => x.SubjectName).Distinct()),
                                AvgScore = g.Average(x => x.AvgScore)
                            })
                            .ToList();

            return groupedData;
        }
    }
}
