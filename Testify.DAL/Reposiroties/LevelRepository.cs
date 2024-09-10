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
        public async Task<Level> GetByidLevel(int id) {
            return await _context.Levels.FindAsync(id);
        }
        public async Task<Level> AddLevel(Level level) {
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
    }
}
