using Microsoft.EntityFrameworkCore;
using System.Buffers;
using System.Xml.Schema;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class LevelRepository
    {
        private readonly TestifyDbContext _context;
        public LevelRepository()
        {
            _context = new TestifyDbContext();
        }



        public async Task<List<Level>> GetAllLevels()
        {
            return await _context.Levels.ToListAsync();
        }
        public async Task<Level> GetByidLevel(int id)
        {
            return await _context.Levels.FindAsync(id);
        }
        public async Task<Level> AddLevel(Level level)
        {
            try
            {
                var addLevel = _context.Levels.Add(level).Entity;
                await _context.SaveChangesAsync();
                return addLevel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Level> UpdateLevel(Level level)
        {
            try
            {
                var objUpdateLevel = await _context.Levels.FindAsync(level.Id);

                objUpdateLevel.Name = level.Name;
                objUpdateLevel.Status = level.Status;


                var updateLevel = _context.Levels.Update(objUpdateLevel).Entity;
                await _context.SaveChangesAsync();
                return updateLevel;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Level> DeleteQuestion(int id)
        {
            try
            {
                var objDeleteLevel = await _context.Levels.FindAsync(id);

                _context.Levels.Remove(objDeleteLevel);
                await _context.SaveChangesAsync();
                return objDeleteLevel;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Level>> GetAllLevelId(int id)
        {
            return await _context.Levels.Where(x => x.Id == id).ToListAsync();
        }

        public async Task<List<User>> GetUserByIdLevel(int levelId, string? textSearch)
        {
            var query = _context.Users.AsQueryable();

            if (levelId >= 0)
            {
                query = query.Where(x => x.LevelId == levelId);
            }

            if (!string.IsNullOrWhiteSpace(textSearch))
            {
                query = query.Where(x => x.FullName.Contains(textSearch) || x.Email.Contains(textSearch) || x.PhoneNumber.Contains(textSearch) || x.UserName.Contains(textSearch));
            }

            return await query.ToListAsync();
        }
    }
}
