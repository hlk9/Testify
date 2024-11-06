using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class ClassUserReposiroty
    {
        TestifyDbContext _context;

        public ClassUserReposiroty()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<ClassUser>> GetAll(byte Status)
        {
            if (Status == 1) //k tìm kiếm và trạng thái hoạt động
            {
                return await _context.ClassUsers.Where(x => x.Status == 1).ToListAsync();
            }
            else if (Status == 10)
            {
                return await _context.ClassUsers.ToListAsync();
            }
            else // k tìm kiếm và trạng thái chờ duyệt
            {
                return await _context.ClassUsers.Where(x => x.Status == 2).ToListAsync();
            }
        }

        public async Task<List<ClassWithClassUser>> GetClassByStudentId(string studentId, string search, byte Status)
        {
            var data = await (from clu in _context.ClassUsers.Where(x => x.UserId == Guid.Parse(studentId) && x.Status == Status)
                              join c in _context.Classes
                              on clu.ClassId equals c.Id
                              join u in _context.Users
                              on c.TeacherId equals u.Id
                              join s in _context.Subjects
                              on c.SubjectId equals s.Id
                              where ((string.IsNullOrEmpty(search) ||
                                    c.Name.ToLower().Contains(search.Trim().ToLower()) ||
                                    u.FullName.ToLower().Contains(search.Trim().ToLower()))
                                    )
                              select new ClassWithClassUser
                              {
                                  ClassId = c.Id,
                                  StudentId = Guid.Parse(studentId),
                                  Name = c.Name,
                                  ClassCode = c.ClassCode,
                                  Description = c.Description,
                                  Capacity = c.Capacity,
                                  TeacherId = c.TeacherId,
                                  TeacherName = u.FullName,
                                  SubjectId = c.SubjectId,
                                  SubjectName = s.Name,
                                  Status = clu.Status
                              }).ToListAsync();

            return data;
        }



        public async Task<ClassUser> Create(ClassUser classUser)
        {
            try
            {
                var obj = _context.ClassUsers.Add(classUser).Entity;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClassUser> UpdateStatusAsync(ClassUser classUser)
        { 
            var _classUser = await _context.ClassUsers.FirstOrDefaultAsync(cu => cu.UserId == classUser.UserId && cu.ClassId == classUser.ClassId);

            _classUser.Status = 1;
            var objUpdateStatus = _context.ClassUsers.Update(_classUser).Entity;
            await _context.SaveChangesAsync();
            return objUpdateStatus; 
        }

        public async Task<ClassUser> DeleteUserInClass(Guid id, int classId)
        {
            try
            {
                var obj = await _context.ClassUsers.FirstOrDefaultAsync(x => x.UserId == id && x.ClassId == classId);

                _context.ClassUsers.Remove(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
