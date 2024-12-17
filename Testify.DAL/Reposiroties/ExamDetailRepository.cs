using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class ExamDetailRepository
    {
        TestifyDbContext _context;
        public ExamDetailRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<ExamDetail>> GetAllExamDetail()
        {
            return await _context.ExamDetails.ToListAsync();
        }

        public async Task<ExamDetail> GetExamDetailId(int id)
        {
            return await _context.ExamDetails.FindAsync(id);
        }

        public async Task<List<ExamDetail>> GetExamDetailByExamId(int examId)
        {
            return await _context.ExamDetails.Where(x => x.ExamId == examId).ToListAsync();
        }

        public async Task<ExamDetail> CreateExamDetail(ExamDetail examDetail)
        {
            try
            {
                var objNew = _context.ExamDetails.Add(examDetail).Entity;
                await _context.SaveChangesAsync();
                return objNew;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ErrorResponse> DeleteExamDetail(int id)
        {
            try
            {
                var isNotDel = await (from exd in _context.ExamDetails
                                      join e in _context.Exams on exd.ExamId equals e.Id
                                      join es in _context.ExamSchedules on e.Id equals es.ExamId
                                      where (exd.Id == id &&
                                      (
                                      (es.StartTime < DateTime.Now && es.EndTime > DateTime.Now) ||
                                      es.StartTime > DateTime.Now
                                      )
                                      )
                                      select exd).AnyAsync();

                var objDelete = await _context.ExamDetails.FindAsync(id);

                if (isNotDel)
                {
                    return new ErrorResponse { Success = false, ErrorCode = "PERMISSION_DENIED", Message = "permission_denied" };
                }

                objDelete.Status = 255;
                _context.ExamDetails.Update(objDelete);
                await _context.SaveChangesAsync();
                return new ErrorResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { Success = false, ErrorCode = "SERVER_ERROR", Message = ex.Message.ToString() };
            }
        }

        public async Task<ExamDetail> UpdateStatus(ExamDetail exam)
        {
            try
            {
                var objUpdate = await _context.ExamDetails.FindAsync(exam.Id);
                objUpdate.Status = exam.Status;
                objUpdate.Code = exam.Code;


                var updateLevel = _context.ExamDetails.Update(objUpdate).Entity;
                await _context.SaveChangesAsync();
                return updateLevel;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsExamDetailCodeExist(string code, int? idExam, int idExamDetail)
        {
            if (idExamDetail == -1)
            {
                var noExamDetail = await (from a in _context.ExamDetails
                                 join b in _context.Exams on a.ExamId equals b.Id
                                 where a.Status != 255 && a.Code.Trim().ToLower().Equals(code.Trim().ToLower()) && b.Id == idExam
                                 select a).AnyAsync();
                return noExamDetail;
            }

            var hasExamDetail = await (from a in _context.ExamDetails
                             join b in _context.Exams on a.ExamId equals b.Id
                             where a.Status != 255 && a.Code.Trim().ToLower().Equals(code.Trim().ToLower()) && b.Id == idExam && a.Id != idExamDetail
                             select a).AnyAsync();

            return hasExamDetail;
        }


    }
}
