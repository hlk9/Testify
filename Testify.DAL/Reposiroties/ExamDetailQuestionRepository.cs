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
    }
}
