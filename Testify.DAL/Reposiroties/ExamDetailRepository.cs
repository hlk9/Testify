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
    } 
}
