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
    public class LecturerRepository
    {
        TestifyDbContext _context;
        public LecturerRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<User>> GetAllLecturer()
        {
            return await _context.Users.Where(x => x.LevelId == 3).ToListAsync();
        }

        public async Task<List<User>> GetAllTeacher()
        {
            return await _context.Users.Where(x => x.LevelId == 3).ToListAsync();
        }
        public async Task<User> GetLecturerById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateLecturer(User user)
        {
            try
            {
                user.LevelId = 3;
                var addLecturer = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addLecturer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateLecturer(User user)
        {
            try
            {
                var updateLecturer = await _context.Users.FindAsync(user.Id);

                updateLecturer.FullName = user.FullName;
                updateLecturer.UserName = user.UserName;
                updateLecturer.DateOfBirth = user.DateOfBirth;
                updateLecturer.PhoneNumber = user.PhoneNumber;
                updateLecturer.Address = user.Address;
                updateLecturer.PasswordHash = user.PasswordHash;
                updateLecturer.AvatarUrl = user.AvatarUrl;
                updateLecturer.LastLogin = user.LastLogin;
                updateLecturer.Email = user.Email;
                updateLecturer.Status = user.Status;
                updateLecturer.LevelId = 3;
                //updateCandidate.Level = user.Level;


                var objLecturer = _context.Users.Update(updateLecturer).Entity;
                await _context.SaveChangesAsync();
                return objLecturer;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public async Task<User> DeleteLecturer(Guid id)
        {
            try
            {
                var objLecturer = await _context.Users.FindAsync(id);

                _context.Users.Remove(objLecturer);
                await _context.SaveChangesAsync();
                return objLecturer;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
