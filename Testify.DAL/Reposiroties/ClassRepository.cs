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
    public class ClassRepository
    {
        private readonly TestifyDbContext _context;
        public ClassRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<Class>> GetAllClass()
        {
            return await _context.Classes.ToListAsync();
        }
        public async Task<Class> GetByIdClass(int id)
        {
            return await _context.Classes.FindAsync(id);
        }
        public async Task<Class> AddClass(Class classes) 
        {
            try
            {
                var addClass = _context.Classes.Add(classes).Entity;
                await _context.SaveChangesAsync();
                return addClass;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Class> UpdateClass(Class classes)
        {
            try
            {
                var objUpdateClass = await _context.Classes.FindAsync(classes.Id);

                objUpdateClass.Name = classes.Name;
                objUpdateClass.ClassCode = classes.ClassCode;
                objUpdateClass.Description = classes.Description;

                var updateClass = _context.Classes.Update(objUpdateClass).Entity;
                await _context.SaveChangesAsync();
                return updateClass;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Class> DeleteClass(int id)
        {
            try
            {
                var objDeleteClass = await _context.Classes.FindAsync(id);

                _context.Classes.Remove(objDeleteClass);
                await _context.SaveChangesAsync();
                return objDeleteClass;
            }
            catch
            {
                return null;
            }
        }
    }
}
