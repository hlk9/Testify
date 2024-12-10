using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

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
                answer.Content = answer.Content.Trim();
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
                obj.Content = answer.Content.Trim();
                obj.IsCorrect = answer.IsCorrect;
                obj.Status = answer.Status;
                obj.UpdatedBy = answer.UpdatedBy;
                obj.UpdatedAt = answer.UpdatedAt;

                var update = _context.Answers.Update(obj).Entity;
                await _context.SaveChangesAsync();
                return update;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Answer> UpdateStatusAnswer(int answerId, byte status)
        {
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

        public async Task<ErrorResponse> DeleteAnswer(int id)
        {
            try
            {
                var objDeleteAnswer = await _context.Answers.FindAsync(id);
                var isInExamDetail = await _context.ExamDetailQuestions.AnyAsync(x => x.QuestionId == objDeleteAnswer.QuestionId);

                if (isInExamDetail)
                {
                    return new ErrorResponse { Success = false, ErrorCode = "PERMISSION_DENIED", Message = "permission_denied" };
                };


                objDeleteAnswer.Status = 255;
                _context.Answers.Update(objDeleteAnswer);
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
