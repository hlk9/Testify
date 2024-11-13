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

        public async Task<List<SubmittedByUser>> GetAllSubmittedByUser(Guid userId)
        {
            var check = await _context.Submissions.FirstOrDefaultAsync(x => x.UserId == userId);
            var check2 = await _context.ExamSchedules.Where(x => x.Id == check.ExamScheduleId).FirstOrDefaultAsync();
            var check3 = await _context.ClassExamSchedules.FirstOrDefaultAsync(x => x.ExamScheduleId == check2.Id);

            var data = await (from submit in _context.Submissions.Where(x => x.UserId == userId)
                              join exs in _context.ExamSchedules on submit.ExamScheduleId equals exs.Id
                              join clex in _context.ClassExamSchedules on exs.Id equals clex.ExamScheduleId
                              join cl in _context.Classes on clex.ClassId equals cl.Id
                              join sub in _context.Subjects on cl.SubjectId equals sub.Id
                              select new SubmittedByUser
                              {
                                  UserId = submit.UserId,
                                  SubmissionId = submit.Id,
                                  ExamScheduleId = submit.ExamScheduleId,
                                  NameExam = exs.Title,
                                  ExamDate = submit.SubmitTime,
                                  SubjectName = sub.Name,
                                  Score = submit.TotalMark,
                                  ClassName = cl.Name
                              }
                              ).ToListAsync();

            return data;
        }

        public async Task<List<Achievenment>> GetAllAchievenment(Guid userId)
        {
            var data = await (from cls in _context.ClassUsers.Where(x => x.UserId == userId && x.Status == 1)
                              join cl in _context.Classes on cls.ClassId equals cl.Id
                              join clexs in _context.ClassExamSchedules on cl.Id equals clexs.ClassId
                              join exs in _context.ExamSchedules on clexs.ExamScheduleId equals exs.Id
                              join submission in _context.Submissions on exs.Id equals submission.ExamScheduleId
                              join sub in _context.Subjects on cl.SubjectId equals sub.Id
                              group submission by new { className = cl.Name, subjectName = sub.Name } into gr
                              select new Achievenment
                              {
                                  ClassName = gr.Key.className,
                                  SubjectName = gr.Key.subjectName,
                                  AvgScore = gr.Any(x => x != null) ? gr.Average(x => x.TotalMark) : 0
                              }
                              ).ToListAsync();

            return data;
        }
    }
}
