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
    public class QuestionReposiroty
    {
        TestifyDbContext _context;

        public QuestionReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            try
            {
                var addQuestion = _context.Questions.Add(question).Entity;
                await _context.SaveChangesAsync();
                return addQuestion;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            try
            {
                var objUpdateQuestion = await _context.Questions.FindAsync(question.Id);

                objUpdateQuestion.Content = question.Content;
                objUpdateQuestion.CreatedDate = question.CreatedDate;
                objUpdateQuestion.Status = question.Status;
                objUpdateQuestion.SubjectId = question.SubjectId;
                objUpdateQuestion.DocumentURL = question.DocumentURL;
                objUpdateQuestion.QuestionLevelId = question.QuestionLevelId;
                objUpdateQuestion.QuestionTypeId = question.QuestionTypeId;

                var updateQuestion = _context.Questions.Update(objUpdateQuestion).Entity;
                await _context.SaveChangesAsync();
                return updateQuestion;
            }
            catch
            {
                return null;
            }
        }


        public async Task<Question> UpdateStatusQuestion(int  questionId, string? status)
        {
            try
            {
                var objUpdateQuestion = await _context.Questions.FindAsync(questionId);

                objUpdateQuestion.Status = Convert.ToBoolean(status);

                var updateQuestion = _context.Questions.Update(objUpdateQuestion).Entity;
                await _context.SaveChangesAsync();
                return updateQuestion;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Question> DeleteQuestion(int id)
        {
            try
            {
                var objDeleteQuestion = await _context.Questions.FindAsync(id);

                _context.Questions.Remove(objDeleteQuestion);
                await _context.SaveChangesAsync();
                return objDeleteQuestion;
            }
            catch 
            {
                return null;
            }
        }
    }
}
