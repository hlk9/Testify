﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class AnswerReposiroty
    {
        TestifyDbContext _context;

        public AnswerReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<Answer>> GetAllAnswers()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<List<Answer>> GetAllAnswerByQuestionId(int questionId)
        {
            return await _context.Answers.Where(x => x.QuestionId == questionId).ToListAsync();
        }

        public async Task<Answer> GetAnSwerById(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<Answer> CreateAnswer(Answer answer)
        {
            try
            {
                var create = _context.Answers.Add(answer).Entity;
                await _context.SaveChangesAsync();
                return create;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<Answer> UpdateAnswer(Answer answer)
        {
            try
            {
                var obj = await _context.Answers.FindAsync(answer.Id);

                obj.QuestionId = answer.QuestionId;
                obj.Content = answer.Content;
                obj.IsCorrect = answer.IsCorrect;
                obj.Status = answer.Status;

                var update = _context.Answers.Update(obj).Entity;
                await _context.SaveChangesAsync();
                return update;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Answer> UpdateStatusAnswer(int answerId, byte status) {
            try
            {
                var obj = await _context.Answers.FindAsync(answerId);

                obj.Status = status;

                var updateStatus = _context.Answers.Update(obj).Entity;
                await _context.SaveChangesAsync();
                return updateStatus;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Answer> DeleteAnswer(int id)
        {
            try
            {
                var delete = await _context.Answers.FindAsync(id);

                _context.Answers.Remove(delete);
                await _context.SaveChangesAsync();
                return delete;
            }
            catch
            {
                return null;
            }
        }
    }
}
