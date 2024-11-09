using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

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
    }
}
