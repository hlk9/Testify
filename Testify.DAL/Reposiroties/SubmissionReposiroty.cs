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
    }
}
