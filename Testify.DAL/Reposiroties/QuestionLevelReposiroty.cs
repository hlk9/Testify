﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class QuestionLevelReposiroty
    {
        TestifyDbContext _context;

        public QuestionLevelReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<QuestionLevel>> GetAllLevels()
        {
            return await _context.QuestionLevels.ToListAsync();
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

        public async Task<QuestionLevel> DeleteLevel(int id)
        {
            try
            {
                var objLevel = await _context.QuestionLevels.FindAsync(id);

                _context.QuestionLevels.Remove(objLevel);
                await _context.SaveChangesAsync();
                return objLevel;
            }
            catch
            {
                return null;
            }
        }
    }
}
