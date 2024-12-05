using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class ExamDetailQuestionRepository
    {
        TestifyDbContext _context;
        public ExamDetailQuestionRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<ExamDetailQuestion>> GetAllByExamDetailId(int examDetailId)
        {
            return await _context.ExamDetailQuestions.Where(x => x.ExamDetailId == examDetailId).ToListAsync();
        }


        public async Task<ExamDetailQuestion> Create(ExamDetailQuestion examDetailQuestion)
        {
            try
            {
                var obj = _context.ExamDetailQuestions.Add(examDetailQuestion).Entity;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteExamDetailQuestionByExamDetailID(int idExamDetail)
        {
            try
            {
                //var objDelete = await _context.ExamDetails.FindAsync(id);
                var relatedQuestions = _context.ExamDetailQuestions.Where(q => q.ExamDetailId == idExamDetail).ToList();

                _context.ExamDetailQuestions.RemoveRange(relatedQuestions);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<QuestionInExam>> GetQuestionByExamDetailID(int examdetailID)
        {
            var data = (from a in _context.ExamDetailQuestions
                        join b in _context.Questions on a.QuestionId equals b.Id
                        join c in _context.QuestionLevels on b.QuestionLevelId equals c.Id
                        join d in _context.QuestionTypes on b.QuestionTypeId equals d.Id
                        join e in _context.ExamDetails on a.ExamDetailId equals e.Id
                        where a.ExamDetailId == examdetailID
                        select new QuestionInExam
                        {
                            Id = a.QuestionId,
                            Content = b.Content,
                            QuestionLevelId = b.QuestionLevelId,
                            QuestionLeveName = c.Name,
                            QuestionTypeId = b.QuestionTypeId,
                            QuestionTypeName = d.Name,
                            CreatedDate = b.CreatedDate,
                            Status = b.Status,
                            SubjectId = b.SubjectId,
                            Point = a.Point,
                        }).ToList();

            return data;
        }


        public async Task<List<QuestionInExam>> GetQuestionByExamDetailID_NOT(int examdetailID, int SubjectId)
        {

            //var data = (from a in _context.ExamDetailQuestions
            //            join b in _context.Questions on a.QuestionId equals b.Id
            //            join c in _context.QuestionLevels on b.QuestionLevelId equals c.Id
            //            join d in _context.QuestionTypes on b.QuestionTypeId equals d.Id
            //            join e in _context.ExamDetails on a.ExamDetailId equals e.Id
            //            where a.ExamDetailId != examdetailID
            //            select new QuestionInExam
            //            {
            //                Id = a.QuestionId,
            //                Content = b.Content,
            //                QuestionLevelId = b.QuestionLevelId,
            //                QuestionLeveName = c.Name,
            //                QuestionTypeId = b.QuestionTypeId,
            //                QuestionTypeName = d.Name,
            //                CreatedDate = b.CreatedDate,
            //                Status = b.Status,
            //                SubjectId = b.SubjectId,
            //                Code = e.Code,
            //            }).Distinct().ToList();
            var _data = (from b in _context.Questions
                         join c in _context.QuestionLevels
                             on b.QuestionLevelId equals c.Id into questionLevelsGroup
                         from c in questionLevelsGroup.DefaultIfEmpty() // Left join
                         join d in _context.QuestionTypes
                             on b.QuestionTypeId equals d.Id
                         where b.Status == 1
                               && !_context.ExamDetailQuestions
                                    .Where(a => a.ExamDetailId == examdetailID)
                                    .Select(a => a.QuestionId)
                                    .Contains(b.Id) // Điều kiện loại trừ QuestionId
                               && b.SubjectId == SubjectId // Điều kiện SubjectId
                         select new QuestionInExam
                         {
                             Id = b.Id,
                             Content = b.Content,
                             QuestionLevelId = b.QuestionLevelId,
                             QuestionLeveName = c != null ? c.Name : null, // Check null
                             QuestionTypeId = b.QuestionTypeId,
                             QuestionTypeName = d.Name,
                             CreatedDate = b.CreatedDate,
                             Status = b.Status,
                             SubjectId = b.SubjectId,
                             // Code = e.Code,
                         }).Distinct().ToList();



            return _data.DistinctBy(x => x.Id).ToList();
        }

        public async Task<List<QuestionInExam>> GetQuestionByExamDetailID_NOTAndLevel(int examdetailID, int levelID,int SubjectId)
        {
            //var data = (from a in _context.ExamDetailQuestions
            //            join b in _context.Questions on a.QuestionId equals b.Id
            //            join c in _context.QuestionLevels on b.QuestionLevelId equals c.Id
            //            join d in _context.QuestionTypes on b.QuestionTypeId equals d.Id
            //            join e in _context.ExamDetails on a.ExamDetailId equals e.Id
            //            where a.ExamDetailId != examdetailID && c.Id == levelID
            //            select new QuestionInExam
            //            {
            //                Id = a.QuestionId,
            //                Content = b.Content,
            //                QuestionLevelId = b.QuestionLevelId,
            //                QuestionLeveName = c.Name,
            //                QuestionTypeId = b.QuestionTypeId,
            //                QuestionTypeName = d.Name,
            //                CreatedDate = b.CreatedDate,
            //                Status = b.Status,
            //                SubjectId = b.SubjectId,
            //                Code = e.Code,
            //            }).Distinct().ToList();

            var _data = (from b in _context.Questions
                         join c in _context.QuestionLevels on b.QuestionLevelId equals c.Id
                         join d in _context.QuestionTypes on b.QuestionTypeId equals d.Id
                         //join e in _context.ExamDetails on b.SubjectId equals e.Id
                         where !_context.ExamDetailQuestions
                                      .Where(a => a.ExamDetailId == examdetailID)
                                      .Select(a => a.QuestionId)
                                      .Contains(b.Id) // Điều kiện loại trừ QuestionId
                               && b.SubjectId == SubjectId && c.Id == levelID // Điều kiện SubjectId
                         select new QuestionInExam
                         {
                             Id = b.Id,
                             Content = b.Content,
                             QuestionLevelId = b.QuestionLevelId,
                             QuestionLeveName = c.Name,
                             QuestionTypeId = b.QuestionTypeId,
                             QuestionTypeName = d.Name,
                             CreatedDate = b.CreatedDate,
                             Status = b.Status,
                             SubjectId = b.SubjectId,
                             //Code = e.Code,
                         }).Distinct().ToList();

            return _data.DistinctBy(x => x.Id).ToList();
        }



        public bool AddListQuestionToExam(List<QuestionInExam> data, int idExamDetail)
        {
            try
            {
                foreach (var h in data)
                {
                    _context.ExamDetailQuestions.Add(new ExamDetailQuestion { QuestionId = h.Id, ExamDetailId = idExamDetail, Point = h.Point });
                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveFromListQuestionToExam(List<QuestionInExam> data, int idExamDetail)
        {
            try
            {
                foreach (var item in data)
                {
                    var a = _context.ExamDetailQuestions.FirstOrDefault(x => x.ExamDetailId == idExamDetail && x.QuestionId == item.Id);
                    if (a != null)
                    {
                        _context.ExamDetailQuestions.Remove(a);
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
