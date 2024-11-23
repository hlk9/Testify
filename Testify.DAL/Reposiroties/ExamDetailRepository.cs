using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;

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

        public async Task<ExamDetail> DeleteExamDetail(int id)
        {
            try
            {
                var objDelete = await _context.ExamDetails.FindAsync(id);
                if (objDelete == null)
                {
                    return null;
                }

                objDelete.Status = 255;
                _context.ExamDetails.Update(objDelete);
                await _context.SaveChangesAsync();
                return objDelete;
            }
            catch
            {
                return null;
            }
        }

    }
}
