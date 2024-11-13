﻿using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class AnswerSubmissionReposiroty
    {
        TestifyDbContext _context;

        public AnswerSubmissionReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<AnswerSubmission> Create(AnswerSubmission answerSubmission)
        {
            try
            {
                var obj = _context.AnswerSubmissions.Add(answerSubmission).Entity;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch
            {
                throw;
            }
        }
    }
}
