using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class QuestionLevelReposiroty
    {
        TestifyDbContext _context;

        public QuestionLevelReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<QuestionLevel>> GetAllLevels(string? textSearch)
        {
            if(string.IsNullOrEmpty(textSearch) || string.IsNullOrWhiteSpace(textSearch))
            {
                return await _context.QuestionLevels
                .Where(x => x.Status == true)
                .ToListAsync();
            }
            else
            {
                return await _context.QuestionLevels
                .Where(x => x.Name.Trim().ToLower().Contains(textSearch.Trim().ToLower()) && x.Status == true)
                .ToListAsync();
            }
            
        }


        public async Task<QuestionLevel> GetLevelById(int id)
        {
            return await _context.QuestionLevels.FindAsync(id);
        }

        public async Task<QuestionLevel> CreateLevel(QuestionLevel QuestionLevel)
        {
            try
            {
                var create = _context.QuestionLevels.Add(QuestionLevel).Entity;
                await _context.SaveChangesAsync();
                return create;
            }
            catch
            {
                return null;
            }
        }

        public async Task<QuestionLevel> UpdateLevel(QuestionLevel QuestionLevel)
        {
            try
            {
                var objLevel = await _context.QuestionLevels.FindAsync(QuestionLevel.Id);

                objLevel.Name = QuestionLevel.Name;
                objLevel.Description = QuestionLevel.Description;
                objLevel.Status = QuestionLevel.Status;

                var update = _context.QuestionLevels.Update(objLevel).Entity;
                await _context.SaveChangesAsync();
                return update;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ErrorResponse> DeleteLevel(int id)
        {
            try
            {
                var objLevel = await _context.QuestionLevels.FindAsync(id);

                var hasQuestionUsed = await _context.Questions.AnyAsync(x => x.QuestionLevelId == id);

                if(hasQuestionUsed)
                {
                    return new ErrorResponse { Success = false, ErrorCode = "PERMISSION_DENIED", Message = "permission_denied" };
                }

                objLevel.Status = false;
                _context.QuestionLevels.Update(objLevel);
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
