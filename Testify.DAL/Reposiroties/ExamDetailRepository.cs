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
                                  ) select exd).AnyAsync();

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

    }
}
