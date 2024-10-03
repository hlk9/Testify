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
    public class SubjectRepository
    {
        TestifyDbContext _context;
        public SubjectRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task< List<Subject>> GetAllSubject(string? textSearch, bool isActive)
        {
            //return await _context.Subjects.ToListAsync();
            if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == false)
            {
                return await _context.Subjects.ToListAsync();
            }
            else if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) || isActive == true)
            {
                return await _context.Subjects.Where(x => x.Status == 1 || x.Status == 255).ToListAsync();
            }
            else if ((textSearch != null || textSearch != "") && isActive == true)
            {
                return await _context.Subjects.Where(x => x.Name.ToLower().Contains(textSearch.Trim().ToLower()) && x.Status == 1 || x.Status == 255 ).ToListAsync();
            }
            else
            {
                return await _context.Subjects.Where(x => x.Name.ToLower().Contains(textSearch.Trim().ToLower())).ToListAsync();
            }


        }

          public async Task<Subject> GetSubjectById(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject> CreateSubject(Subject sub)
        {
            try
            {
                var addSubject = _context.Subjects.Add(sub).Entity;
                await _context.SaveChangesAsync();
                return addSubject;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Subject> UpdateSubject(Subject sub)
        {
            try
            {
               var updateSubject = _context.Subjects.Find(sub.Id); 
                
                updateSubject.Name = sub.Name;
                updateSubject.Description = sub.Description;
                updateSubject.Status = sub.Status;           
                var objSubject = _context.Subjects.Update(updateSubject).Entity;
                await _context.SaveChangesAsync();
                return objSubject;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Subject> DeleteSubject(int id)
        {
            try
            {
                var deleteSubject =_context.Subjects.Find(id);

                var objSubject = _context.Subjects.Remove(deleteSubject).Entity;
                await _context.SaveChangesAsync();
                return objSubject;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
