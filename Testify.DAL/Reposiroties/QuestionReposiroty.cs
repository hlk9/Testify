using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class QuestionReposiroty
    {
        TestifyDbContext _context;
        ExamDetailQuestionRepository _repoExamDetailQuestion;

        public QuestionReposiroty()
        {
            _context = new TestifyDbContext();
            _repoExamDetailQuestion = new ExamDetailQuestionRepository();
        }

        public async Task<List<Question>> GetAllQuestions(string? textSearch, bool isActive)
        {
            if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == false)
            {
                return await _context.Questions.ToListAsync();
            }
            else if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == true)
            {
                return await _context.Questions.Where(x => x.Status == 1 || x.Status == 255).ToListAsync();
            }
            else if ((textSearch != null || textSearch != "") && isActive == true)
            {
                return await _context.Questions.Where(x => x.Content.ToLower().Contains(textSearch.Trim().ToLower()) && x.Status == 1 || x.Status == 255).ToListAsync();
            }
            else
            {
                return await _context.Questions.Where(x => x.Content.ToLower().Contains(textSearch.Trim().ToLower())).ToListAsync();
            }
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<List<Question>> GetQuestionBySubjectId(int subjectId)
        {
            return await _context.Questions.Where(x => x.SubjectId == subjectId && x.Status == 1).ToListAsync();
        }

        public async Task<List<Question>> GetQuestionBySubjectIdAndLevel(int subjectId, int levelId)
        {
            return await _context.Questions.Where(x => x.SubjectId == subjectId && x.QuestionLevelId == levelId && x.Status == 1).ToListAsync();
        }

        //public async Task<List<QuestionInExam>> GetQuesBySub_Level_CHosen(int subID, int levelId, bool choosen)
        //{
        //    return await _context.Questions
        //}

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
                objUpdateQuestion.DocumentPath = question.DocumentPath;
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

        public async Task<Question> UpdateDocumentPath(Question question)
        {
            try
            {
                var objUpdateQuestion = await _context.Questions.FindAsync(question.Id);
                objUpdateQuestion.DocumentPath = question.DocumentPath;

                var updateQuestion = _context.Questions.Update(objUpdateQuestion).Entity;
                await _context.SaveChangesAsync();
                return updateQuestion;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Question> UpdateStatusQuestion(int questionId, byte status)
        {
            try
            {
                var objUpdateQuestion = await _context.Questions.FindAsync(questionId);

                objUpdateQuestion.Status = status;

                var updateQuestion = _context.Questions.Update(objUpdateQuestion).Entity;
                await _context.SaveChangesAsync();
                return updateQuestion;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ErrorResponse> DeleteQuestion(int id)
        {
            try
            {
                var objDeleteQuestion = await _context.Questions.FindAsync(id);
                var isInExamDetail = await _context.ExamDetailQuestions.AnyAsync(x => x.QuestionId == id);

                if (isInExamDetail)
                {
                    return new ErrorResponse { Success = false, ErrorCode = "PERMISSION_DENIED", Message = "permission_denied" };
                };


                objDeleteQuestion.Status = 255;
                _context.Questions.Update(objDeleteQuestion);
                await _context.SaveChangesAsync();
                return new ErrorResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { Success = false, ErrorCode = "SERVER_ERROR", Message = ex.Message.ToString() };
            }
        }

        public async Task<AnswerAndQuestion> GetQuestionWithTrueAnswer(int questionId, int examDetailId)
        {
            var answerIsTrue = await (from q in _context.Questions.Where(x => x.Status == 1)
                                      join a in _context.Answers
                                      on q.Id equals a.QuestionId
                                      where q.Id == questionId
                                      && a.IsCorrect == true
                                      select a
                                      ).ToListAsync();

            var question = await GetQuestionById(questionId);
            var lstexamDetailQuestion = await _repoExamDetailQuestion.GetAllByExamDetailId(examDetailId);
            var pointOfQuestion = lstexamDetailQuestion.FirstOrDefault(x => x.QuestionId == questionId).Point;

            AnswerAndQuestion QnA = new AnswerAndQuestion()
            {
                QuestionId = question.Id,
                ContentQuestion = question.Content,
                QuestionTypeId = question.QuestionTypeId,
                LstAnswer = answerIsTrue,
                PointOfQuestion = pointOfQuestion
            };

            return QnA;
        }

        public async Task<List<QuestionInExcel>> Check(int subjectId)
        {
            var data = (from q in _context.Questions.Where(x => x.SubjectId == subjectId && x.Status == 1)
                        join a in _context.Answers
                        on q.Id equals a.QuestionId

                        select new
                        {
                            q.Content,
                            q.Id,
                            q.QuestionTypeId,
                            q.SubjectId,
                            Answer = a
                        }).ToList();

            var groupAnswer = data.GroupBy(q => new { q.Content, q.QuestionTypeId, q.Id, q.SubjectId }).Select(g => new QuestionInExcel
            {
                QuestionId = g.Key.Id,
                QuestionTypeId = g.Key.QuestionTypeId,
                SubjectId = g.Key.SubjectId,
                Content = g.Key.Content,
                Answers = g.Select(x => x.Answer).ToList(),
            }).ToList();

            return groupAnswer;
        }

        public async Task<int> GetCountQuestionByUserId(Guid userId)
        {
            var objUser = _context.Users.Find(userId);

            if (objUser.LevelId == 1 || objUser.LevelId == 2)
            {
                var allQuestion = _context.Questions.ToList();
                return allQuestion.Count;
            }

            return -1;
        }
    }
}
