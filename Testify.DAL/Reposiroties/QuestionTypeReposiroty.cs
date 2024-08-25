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
    public class QuestionTypeReposiroty
    {
        TestifyDbContext _context;

        public QuestionTypeReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<QuestionType>> GetAllTypes()
        {
            return await _context.QuestionTypes.ToListAsync();
        }

        public async Task<QuestionType> GetTypeById(int id)
        {
            return await _context.QuestionTypes.FindAsync(id);
        }

        public async Task<QuestionType> CreateType(QuestionType questionType)
        {
            try
            {
                var create = _context.QuestionTypes.Add(questionType).Entity;
                await _context.SaveChangesAsync();
                return create;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<QuestionType> UpdateType(QuestionType questionType)
        {
            try
            {
                var objType = await _context.QuestionTypes.FindAsync(questionType.Id);

                objType.Name = questionType.Name;
                objType.Description = questionType.Description;
                objType.Status = questionType.Status;

                var update = _context.QuestionTypes.Update(objType).Entity;
                await _context.SaveChangesAsync();
                return update;
            }
            catch
            {
                return null;
            }
        }

        public async Task<QuestionType> DeleteType(int id)
        {
            try
            {
                var objType = await _context.QuestionTypes.FindAsync(id);

                _context.QuestionTypes.Remove(objType);
                await _context.SaveChangesAsync();
                return objType;
            }
            catch
            {
                return null;
            }
        }
    }
}
