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

        public async Task< List<Subject>> GetAllSubject()
        {
            return await _context.Subjects.ToListAsync()    ;
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
                updateSubject.OrganizationId = sub.OrganizationId;
                updateSubject.OrganizationId = sub.OrganizationId;

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
